using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SeeSharp.Application.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SeeSharp.Infrastructure.Identity;

public class JwtUtils : IJwtUtils
{
    //private readonly string _key;
    //private readonly string _issuer;
    //private readonly string _audience;
    //private readonly string _expiryMinutes;
    private readonly IConfiguration _configuration;

    public JwtUtils(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string userId, string fullName, string userName, IList<string> roles)
    {
        var jwtSettings = _configuration.GetSection("JwtOptions");
        Guard.Against.Null(jwtSettings, message: "JwtOptions not found.");

        var key = Guard.Against.NullOrEmpty(jwtSettings["Secret"], message: "'Secret' not found or empty.");
        var issuer = Guard.Against.NullOrEmpty(jwtSettings["Issuer"], message: "'Issuer' not found or empty.");
        var audience = Guard.Against.NullOrEmpty(jwtSettings["Audience"], message: "'Audience' not found or empty.");
        var expiryMinutes = Guard.Against.NullOrEmpty(jwtSettings["expiryInMinutes"], message: "'expiryInMinutes' not found or empty.");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Jti, userId),
            new Claim("Name", fullName),
            new Claim("UserId", userId),
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(expiryMinutes)),
            signingCredentials: signingCredentials
            );

        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return encodedToken;
    }

    public List<string> ValidateToken(string token)
    {

        if (token == null)
            return new List<string>();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSettings = _configuration.GetSection("JwtOptions");
        Guard.Against.Null(jwtSettings, message: "JwtOptions not found.");
        var key = Guard.Against.NullOrEmpty(jwtSettings["Secret"], message: "'Secret' not found or empty.");

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false,

            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        if (jwtToken != null)
        {
            var roles = new List<string>();
            foreach (var claim in jwtToken.Claims)
            {
                if (claim.Type.ToLower() == "role")
                {
                    roles.Add(claim.Value);
                }
            }
            return roles;
        }

        // return user roles from JWT token if validation successful
        return new List<string>();
    }
}
