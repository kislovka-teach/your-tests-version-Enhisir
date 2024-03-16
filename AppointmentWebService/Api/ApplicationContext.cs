using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api;

public sealed class ApplicationContext : DbContext
{
    public DbSet<Specialization> Specializations { get; set; } = null!;
    
    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    
    public DbSet<Visit> Appointments { get; set; }
    public DbSet<Review> Reviews { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) 
        :base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Visit>()
            .HasOne(v => v.Review)
            .WithOne(r => r.Visit)
            .HasForeignKey<Review>(r => r.Id);
    }
}