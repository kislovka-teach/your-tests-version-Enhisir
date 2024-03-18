using Api.Models;

namespace Api.Services;

public class ValidateLoginService(
    IPlayerRepository userService,
    IPasswordHasherService passwordHasherService) : IValidateLoginService
{
    public async Task<Player?> ValidateLoginAsync(string username, string password)
    {
        var player = await userService.GetConcretePlayerAsync(username);
        
        if (player is null 
            || !passwordHasherService
                .Validate(player.PasswordHashed, password))
            return null;
        
        return player;
    }
}