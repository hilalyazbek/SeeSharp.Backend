using Ardalis.GuardClauses;
using Google.Apis.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;
using SeeSharp.Application.Common.Exceptions;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Application.Features.Authentication.Commands.GoogleAuthUserCommand;

namespace SeeSharp.Application.Features.Authentication.Commands.AuthUserCommand;

public record GoogleAuthUserCommand(string GoogleToken) : IRequest<GoogleAuthResponseDto>;

public class GoogleAuthUserCommandHandler : IRequestHandler<GoogleAuthUserCommand, GoogleAuthResponseDto>
{
    private readonly IConfiguration _configuration;
    private readonly IIdentityService _identityService;
    private readonly IJwtUtils _jwtUtils;

    public GoogleAuthUserCommandHandler(IIdentityService identityService, IJwtUtils jwtUtils, IConfiguration configuration)
    {
        _identityService = identityService;
        _jwtUtils = jwtUtils;
        _configuration = configuration;
    }

    public async Task<GoogleAuthResponseDto> Handle(GoogleAuthUserCommand request, CancellationToken cancellationToken)
    {
        var googleAuthOptions = Guard.Against.Null(_configuration.GetSection("GoogleAuthOptions"));
        var clientId = Guard.Against.NullOrEmpty(googleAuthOptions["ClientId"]);

        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { clientId }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(request.GoogleToken, settings);

        var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsByEmailAsync(payload.Email);

        if (userId == null)
        {
            throw new BadRequestException("User does not exist");
        }

        string token = _jwtUtils.GenerateToken(userId, fullName, userName, roles);

        return new GoogleAuthResponseDto()
        {
            UserId = userId,
            Name = fullName,
            Email = email,
            Roles = roles,
            Token = token
        };
    }
}