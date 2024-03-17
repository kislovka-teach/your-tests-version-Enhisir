using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class GameRepository(ApplicationContext dbContext) : IGameRepository
{
    public async Task<List<Game>> GetGamesAsync(Company? developer, Company? publisher)
    {
        var query = dbContext.Games.AsQueryable();

        if (developer is not null)
        {
            query = query
                .Include(g => g.Developer)
                .Where(g => developer.Id == g.DeveloperId);
        }
        
        if (publisher is not null)
        {
            query = query
                .Include(g => g.Publisher)
                .Where(g => publisher.Id == g.PublisherId);
        }

        return await query.ToListAsync();
    }
    
    public async Task<Game?> GetConcreteGameAsync(int id)
    {
        return await dbContext.Games.SingleOrDefaultAsync(e => id == e.Id);
    }

    public void AddGame(Game game)
    {
        dbContext.Games.Add(game);
        dbContext.SaveChanges();
    }
}