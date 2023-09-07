using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SeeSharp.Application.Common.Exceptions;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Application.Common.Models;
using SeeSharp.Domain.Constants;
using SeeSharp.Infrastructure.Identity;
using System.Data;

namespace SeeSharp.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<(string UserId,string FullName, string UserName, string Email, IList<string> Roles)> GetUserDetailsByUserNameAsync(string userName)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        var roles = await _userManager.GetRolesAsync(user);

        return (user.Id!,user.FullName!, user.UserName!, user.Email!, roles);
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string fullName, string userName, string email, string password)
    {
        var user = new ApplicationUser
        {
            FullName = fullName,
            UserName = userName,
            Email = email,
        };

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> IsInRoleAsync(string userName, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.UserName == userName);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthenticateAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if(user == null)
        {
            return false;
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, true, false);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<Result> AddToRolesAsync(string userId, List<string> roles)
    {
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
        if(user == null)
        {
            return Result.Failure(new List<string> { "User not found" });
        }

        foreach(var role in roles)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role);
            if (!roleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        
        var result = await _userManager.AddToRolesAsync(user, roles);

        return result.ToApplicationResult();
    }
}