using Microsoft.AspNetCore.Http;
using SeeSharp.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharp.Infrastructure.Identity;
internal class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

        if (userId != null && Guid.TryParse(userId, out var parsedUserId))
        {
            return parsedUserId;
        }

        throw new InvalidOperationException("User is not authenticated or UserId is invalid.");
    }
}
