using System.Security.Claims;
using Api.Dtos;
using Api.Models;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Groups;

public static class GamesGroup
{
    public static RouteGroupBuilder MapGames(this RouteGroupBuilder group)
    {
        group.MapGet("", GetGames);
        group.MapPost("", AddGame).AddEndpointFilter<AdminEndpointFilter>();
        group.MapPost("{gameId:int}/note", AddGameNote).AddEndpointFilter<AuthEndpointFilter>();

        return group;
    }

    private static async Task GetGames(
        HttpContext context,
        [FromQuery(Name = "dev")] int? devId,
        [FromQuery(Name = "pub")] int? pubId,
        IGameRepository gameRepository,
        ICompanyRepository companyRepository)
    {
        var dev = devId is null
            ? null
            : await companyRepository.GetCompanyAsync(devId.Value);

        var pub = pubId is null
            ? null
            : await companyRepository.GetCompanyAsync(pubId.Value);

        if (devId is not null && dev is null || pubId is not null && pub is null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }

        var games = await gameRepository.GetGamesAsync(dev, pub);

        await context.Response.WriteAsJsonAsync(games);
    }
    
    private static async Task GetConcreteGame(
        HttpContext context,
        int gameId,
        IGameRepository gameRepository)
    {
        var game = await gameRepository.GetConcreteGameAsync(gameId);
        
        if (game is null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }

        await context.Response.WriteAsJsonAsync(game);
    }

    private static async Task AddGame(
        HttpContext context,
        [FromBody] GameDto dto,
        IMapper mapper,
        IGameRepository gameRepository,
        ICompanyRepository companyRepository)
    {
        var dev = await companyRepository.GetCompanyAsync(dto.DeveloperId);
        var pub = await companyRepository.GetCompanyAsync(dto.PublisherId);

        if (dev is null || pub is null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }

        var game = mapper.Map<Game>(dto);
        gameRepository.AddGame(game);
        context.Response.StatusCode = StatusCodes.Status200OK;
    }
    
    private static async Task AddGameNote(
        HttpContext context,
        int gameId,
        [FromBody] GameNoteDto dto,
        IMapper mapper,
        IGameRepository gameRepository,
        IGameNoteRepository gameNoteRepository)
    {
        var username = context.User.FindFirst(ClaimTypes.Name)!;
        var game = await gameRepository.GetConcreteGameAsync(gameId);
        if (game is null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }

        var gameNote = mapper.Map<GameNote>(dto);
        gameNote.PlayerId = username.Value;
        gameNoteRepository.AddGameNote(gameNote);
        context.Response.StatusCode = StatusCodes.Status200OK;
    }
}