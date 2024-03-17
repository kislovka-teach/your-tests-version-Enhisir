using Api.Models;

namespace Api.Services;

public interface IPlayerRepository
{
    public Task<Player?> GetConcretePlayer(string username);
    public void AddPlayer(Player player);
}