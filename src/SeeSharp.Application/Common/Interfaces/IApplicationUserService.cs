namespace SeeSharp.Application.Common.Interfaces;
public interface IApplicationUserService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<(string UserId, string FullName, string UserName, string Email, IList<string> Roles)> GetUserDetailsByUserIdAsync(string userId);

    Task<(string UserId, string FullName, string UserName, string Email, IList<string> Roles)> GetUserDetailsByUserNameAsync(string userName);

    Task<(string UserId, string FullName, string UserName, string Email, IList<string> Roles)> GetUserDetailsByEmailAsync(string userName);
}
