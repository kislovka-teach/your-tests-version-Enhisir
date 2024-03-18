using Api.Models;

namespace Api.Services;

public interface IPlayerRepository
{
    public Task<Player?> GetConcretePlayerAsync(string username);
    public void AddPlayer(Player player);
}