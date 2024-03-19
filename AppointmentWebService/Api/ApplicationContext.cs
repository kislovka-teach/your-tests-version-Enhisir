using Api.Models;
using Api.Services;
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
        
        modelBuilder
            .Entity<Visit>()
            .HasOne<Patient>()
            .WithMany(e => e.Visits)
            .HasForeignKey(v => v.PatientUserName);

        var passwordHasher = new PasswordHasherService();
        modelBuilder
            .Entity<Patient>()
            .HasData(
                new Patient()
                {
                    Name = "m", 
                    Surname = "s",
                    UserName = "enhisir", 
                    PasswordHashed = passwordHasher.Hash("enhisir")
                },
                new Patient()
                {
                    Name = "n",
                    Surname = "i",
                    UserName = "nikoimam",
                    PasswordHashed = passwordHasher.Hash("nikoimam")
                });

        modelBuilder.Entity<Specialization>().HasData(
            new Specialization() { Id = 1, Name = "Handsome master" }
            );

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor()
            {
                SpecializationId = 1, 
                Name = "Johnny", 
                Surname = "Sins", 
                UserName = "bold", 
                PasswordHashed = passwordHasher.Hash("bold")
            });

        modelBuilder.Entity<Visit>().HasData(
            new Visit()
            {
                Id = 1, 
                PatientUserName = "enhisir",
                DoctorUserName = "bold", 
                Date = DateTime.Today.ToUniversalTime(), 
                IsSuccessful = true,
                Finding = "pomer..."
            }
        );
    }
}