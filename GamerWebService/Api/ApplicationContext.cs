using Api.Models;
using Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Api;

public sealed class ApplicationContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<GameNote> GameNotes { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) 
        :base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Player>()
            .HasKey(e => e.UserName);
        
        modelBuilder
            .Entity<GameNote>()
            .HasKey(e => new { e.GameId, e.PlayerId });

        var passwordHasher = new PasswordHasherService();

        var me = new Player() { UserName = "enhisir", PasswordHashed = passwordHasher.Hash("enhisir") };
        
        modelBuilder
            .Entity<Player>()
            .HasData(
                me,
                new Player() { UserName = "nikoimam", PasswordHashed = passwordHasher.Hash("nikoimam") });

        var lubimiePsheki = new Company() { Id = 1, Name = "CD Projekt RED" };
        var prosrali = new Company() { Id = 2, Name = "Konami" };
        var boogie = new Company() { Id = 3, Name = "Bungie" };
        var billieBoys = new Company() { Id = 4, Name = "Xbox Game Studios" };
        var nashi = new Company() { Id = 5, Name = "Perelesoq" };

        modelBuilder.Entity<Company>().HasData(lubimiePsheki, prosrali, boogie, billieBoys, nashi);

        modelBuilder.Entity<Game>().HasData(
            new Game() { Id = 1, Title = "Cyberpunk 2077", DeveloperId = 1, PublisherId = 1 },
            new Game() { Id = 2, Title = "Metal Gear Solid", DeveloperId = 2, PublisherId = 2 },
            new Game() { Id = 3, Title = "Halo: Combat Evolved", DeveloperId = 3, PublisherId = 4 },
            new Game() { Id = 4, Title = "Torn Away", DeveloperId = 5, PublisherId = 5 });

        modelBuilder.Entity<GameNote>().HasData(
            new GameNote() { GameId = 1, PlayerId = "enhisir", Completed = true, Favourite = true, PlayLater = true },
            new GameNote() { GameId = 3, PlayerId = "enhisir", Completed = false, Favourite = false, PlayLater = true }
        );
    }
}