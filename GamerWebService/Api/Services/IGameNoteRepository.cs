using Api.Models;

namespace Api.Services;

public interface IGameNoteRepository
{
    public Task<List<GameNote>> GetGameNotes(string playerId);
    public Task<GameNote?> GetConcreteGameNote(int gameId, string playerId);
    public void AddGameNote(GameNote gameNote);
    public void UpdateGameNote(GameNote gameNote);
}