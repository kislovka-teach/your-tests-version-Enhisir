using Api.Models;
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
    }
}