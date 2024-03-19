using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class GameNoteRepository(ApplicationContext applicationContext) : IGameNoteRepository
{
    public async Task<List<GameNote>> GetGameNotes(string playerId)
    {
        return await applicationContext.GameNotes
            .Include(e => e.Game)
            .Where(e => playerId == e.PlayerId)
            .ToListAsync();
    }
    
    public async Task<GameNote?> GetConcreteGameNote(int gameId, string playerId)
    {
        return await applicationContext.GameNotes
            .Include(e => e.Game)
            .SingleOrDefaultAsync(e => gameId == e.GameId
                                       && playerId == e.PlayerId);
    }

    public void AddGameNote(GameNote gameNote)
    {
        applicationContext.Add(gameNote);
        applicationContext.SaveChanges();
    }

    public void UpdateGameNote(GameNote gameNote)
    {
        applicationContext.Update(gameNote);
        applicationContext.SaveChanges();
    }
}