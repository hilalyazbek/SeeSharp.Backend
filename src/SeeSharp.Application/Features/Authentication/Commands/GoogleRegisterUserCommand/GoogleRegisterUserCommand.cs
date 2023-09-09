using Ardalis.GuardClauses;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SeeSharp.Application.Common.Interfaces;

namespace SeeSharp.Application.Features.Authentication.Commands.GoogleRegisterUserCommand;

public record GoogleRegisterUserCommand(string GoogleToken) : IRequest<string>;

public class GoogleRegisterUserCommandHandler : IRequestHandler<GoogleRegisterUserCommand, string>
{
    private readonly IConfiguration _configuration;
    private readonly IIdentityService _identityService;

    public GoogleRegisterUserCommandHandler(IIdentityService identityService, IConfiguration configuration)
    {
        _identityService = identityService;
        _configuration = configuration;
    }

    public async Task<string> Handle(GoogleRegisterUserCommand request, CancellationToken cancellationToken)
    {
        var googleAuthOptions = Guard.Against.Null(_configuration.GetSection("GoogleAuthOptions"));
        var clientId = Guard.Against.NullOrEmpty(googleAuthOptions["ClientId"]);

        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { clientId }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(request.GoogleToken, settings);
        
        if(payload == null){

            throw new Exception($"Error while authenticating with Google.");
        }
        
        var result = await _identityService.CreateExternalUserAsync(payload.Name, payload.Email, payload.Email, "Google", clientId, "Google");

        if (!result.Result.Succeeded)
        {
            var errors = string.Join(Environment.NewLine, result.Result.Errors);

            throw new Exception($"Unable to create {payload.Email}.{Environment.NewLine}{errors}");
        }

        var addUserToRole = await _identityService.AddToRolesAsync(result.UserId, new List<string> { "Anonymous" });
        if(addUserToRole == null)
        {
            var errors = string.Join(Environment.NewLine, addUserToRole!.Errors);

            throw new Exception($"Unable to add {payload.Email} to assigned role/s.{Environment.NewLine}{errors}");
        }

        return result.UserId;
    }
}