using Api.Models;

namespace Api.Services;

public interface IGameRepository
{
    public Task<List<Game>> GetGamesAsync(Company? developer, Company? publisher);
    public Task<Game?> GetConcreteGameAsync(int id);
    public void AddGame(Game game);
}