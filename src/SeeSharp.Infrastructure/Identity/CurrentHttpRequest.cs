using Microsoft.AspNetCore.Http;
using SeeSharp.Application.Common.Interfaces;
using SeeSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SeeSharp.Infrastructure.Identity;
internal class CurrentHttpRequest : ICurrentHttpRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentHttpRequest(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserId()
    {
        return _httpContextAccessor.HttpContext!.User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

    }
}
