using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PlayerRepository(ApplicationContext dbContext) : IPlayerRepository
{
    public async Task<Player?> GetConcretePlayerAsync(string username)
        => await dbContext.Players
            .SingleOrDefaultAsync(e => username.Equals(e.UserName));
    

    public void AddPlayer(Player player)
    {
        dbContext.Players.Add(player);
        dbContext.SaveChanges();
    }
}