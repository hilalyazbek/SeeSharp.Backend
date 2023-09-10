using Ardalis.GuardClauses;
using Google.Apis.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Constants;

namespace SeeSharp.Application.Features.Authentication.Commands.SignInWithGoogleCommand;

public record SignInWithGoogleCommand(string GoogleToken) : IRequest<GoogleAuthResponseDto>;

public class SignInWithGoogleCommandHandler : IRequestHandler<SignInWithGoogleCommand, GoogleAuthResponseDto>
{
    private readonly IConfiguration _configuration;
    private readonly IIdentityService _identityService;
    private readonly IJwtUtils _jwtUtils;

    public SignInWithGoogleCommandHandler(IIdentityService identityService, IJwtUtils jwtUtils, IConfiguration configuration)
    {
        _identityService = identityService;
        _jwtUtils = jwtUtils;
        _configuration = configuration;
    }

    public async Task<GoogleAuthResponseDto> Handle(SignInWithGoogleCommand request, CancellationToken cancellationToken)
    {
        var googleAuthOptions = Guard.Against.Null(_configuration.GetSection("GoogleAuthOptions"));
        var clientId = Guard.Against.NullOrEmpty(googleAuthOptions["ClientId"]);

        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { clientId }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(request.GoogleToken, settings);

        var userExists = await _identityService.UserExists(payload.Email);
        
        if (!userExists)
        {
            var result = await _identityService.CreateExternalUserAsync(payload.Name, payload.Email, payload.Email, "Google", payload.JwtId, "Google");

            if (!result.Result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.Result.Errors);

                throw new Exception($"Unable to create {payload.Email}.{Environment.NewLine}{errors}");
            }

            var addUserToRole = await _identityService.AddToRolesAsync(result.UserId, new List<string> { Roles.BasicUser });
            if (addUserToRole == null)
            {
                var errors = string.Join(Environment.NewLine, addUserToRole!.Errors);

                throw new Exception($"Unable to add {payload.Email} to assigned role/s.{Environment.NewLine}{errors}");
            }
        }

        var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsByEmailAsync(payload.Email);

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