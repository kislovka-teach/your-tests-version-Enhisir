using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api;

public sealed class ApplicationContext : DbContext
{
    public DbSet<Specialization?> Specializations { get; set; } = null!;
    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Visit> Visits { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) 
        :base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .HasKey(e => e.UserName);
    }
}