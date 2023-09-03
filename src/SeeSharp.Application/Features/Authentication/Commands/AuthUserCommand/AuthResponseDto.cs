namespace SeeSharp.Application.Features.Authentication.Commands.AuthUserCommand;

public class AuthResponseDto{
    public string? UserId { get; set; }

    public string? Name { get; set; }

    public IList<string> Roles { get; set; } = new List<string>();

    public string? Token { get; set; }
}