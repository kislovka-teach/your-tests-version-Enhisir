using System.Security.Claims;
using Api.Dtos;
using Api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Api.Groups;

public static class AuthGroup
{
    
    public static RouteGroupBuilder MapAuthentication(this RouteGroupBuilder group)
    {
        group.MapPost("login", Login);
        return group;
    }

    private static async Task Login(
        HttpContext context,
        [FromBody] LoginDto dto,
        IPlayerRepository userService, 
        IPasswordHasherService passwordHasherService)
    {
        var player = await userService.GetConcretePlayer(dto.UserName);
        
        if (player is not null 
            && passwordHasherService.Validate(player.PasswordHashed, dto.Password))
        {
            var usernameClaim = new Claim(ClaimTypes.Name, player.UserName);
            var roleClaim = new Claim(ClaimTypes.Role, player.Role.ToString());

            var userIdentity = new ClaimsIdentity(
                new []{ usernameClaim, roleClaim },
                "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(userIdentity);
            
            await context.SignInAsync(claimsPrincipal);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var customResponse = new
            {
                status = 401,
                message = "Unauthorized. Please Provide Valid Credentials"
            };

            await context.Response.WriteAsJsonAsync(customResponse);
        }
    }
}