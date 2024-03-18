using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class JwtTokenService(IConfiguration config) : IJwtTokenService
{
    public JwtSecurityToken GetJwtSecurityToken(string username, Role role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role.ToString())
        };
        
        return new JwtSecurityToken(
            issuer: AuthOptions.Issuer, 
            audience: AuthOptions.Audience, 
            claims: claims, 
            expires: AuthOptions.Expires, 
            signingCredentials: new SigningCredentials(
                AuthOptions.SecurityKey, 
                SecurityAlgorithms.HmacSha256));
    }
}