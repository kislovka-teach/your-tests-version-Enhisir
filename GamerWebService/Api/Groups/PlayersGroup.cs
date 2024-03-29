using Api.Services;

namespace Api.Groups;

public static class PlayersGroup
{
    public static RouteGroupBuilder MapPlayers(this RouteGroupBuilder group)
    {
        group.MapGet("{username}", GetPlayer);
        group.MapGet("{username}/games", GetGames);
        group.MapGet("{username}/games/{gameId:int}", GetGameNote);
        return group;
    }
    
    private static async Task GetPlayer(
        HttpContext context,
        string username,
        IPlayerRepository playerRepository)
    {
        var player = await playerRepository.GetConcretePlayerAsync(username);

        if (player is null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }

        await context.Response.WriteAsJsonAsync(player);
    }

    private static async Task GetGames(
        HttpContext context,
        string username,
        IPlayerRepository playerRepository,
        IGameNoteRepository gameNoteRepository)
    {
        var player = await playerRepository.GetConcretePlayerAsync(username);

        if (player is null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }

        var games = 
            (await gameNoteRepository
                .GetGameNotes(player.UserName))
            .Select(e => e.Game);

        await context.Response.WriteAsJsonAsync(games);
    }
    
    private static async Task GetGameNote(
        HttpContext context,
        string username,
        int gameId,
        IGameNoteRepository gameNoteRepository)
    {
        var gameNote = await gameNoteRepository.GetConcreteGameNote(gameId, username);

        if (gameNote is null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }
        
        await context.Response.WriteAsJsonAsync(gameNote);
    }
}