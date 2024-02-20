namespace SeeSharp.Application.Features.UserManagement.Queries;
public class ApplicationUserDto
{
    public string? UserId { get; set; }
    public string? FullName { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public IList<string>? Roles { get; set; } = new List<string>();
}
