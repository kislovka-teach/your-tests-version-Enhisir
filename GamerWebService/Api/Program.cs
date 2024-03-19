using System.IdentityModel.Tokens.Jwt;
using Api;
using Api.Dtos;
using Api.Groups;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<ApplicationContext>(optionsBuilder => 
        optionsBuilder.UseNpgsql(
            builder.Configuration
                .GetConnectionString("GamerWebService")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.Issuer,

        ValidateAudience = true,
        ValidAudience = AuthOptions.Audience,

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = AuthOptions.SecurityKey
    };
});
builder.Services.AddAuthorization();

builder.Services
    .AddScoped<ICompanyRepository, CompanyRepository>()
    .AddScoped<IGameRepository, GameRepository>()
    .AddScoped<IGameNoteRepository, GameNoteRepository>()
    .AddScoped<IPlayerRepository, PlayerRepository>()
    .AddScoped<IValidateLoginService, ValidateLoginService>()
    .AddScoped<IPasswordHasherService, PasswordHasherService>()
    .AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddAutoMapper(
    options => options.AddProfile<MapperProfile>());

var app = builder.Build();

app.MapPost(
    "/login", 
    async (HttpContext context,
        [FromBody] LoginDto dto, 
        IValidateLoginService validateLoginService,
        IJwtTokenService jwtTokenService) =>
    {
        var player = await validateLoginService
            .ValidateLoginAsync(dto.UserName, dto.Password);
        if (player is null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        var jwt = jwtTokenService.GetJwtSecurityToken(player.UserName, player.Role);
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        context.Response.Headers.Add(new KeyValuePair<string, StringValues>("Authorization",  "Bearer " + token));
        context.Response.StatusCode = StatusCodes.Status200OK;
    });

app.MapGroup("games").MapGames();
app.MapGroup("companies").MapCompanies();
app.MapGroup("players").MapPlayers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();