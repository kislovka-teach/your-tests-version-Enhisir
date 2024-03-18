using System.IdentityModel.Tokens.Jwt;
using Api.Models;

namespace Api.Services;

public interface IJwtTokenService
{
    public JwtSecurityToken GetJwtSecurityToken(string username, Role Role);
}