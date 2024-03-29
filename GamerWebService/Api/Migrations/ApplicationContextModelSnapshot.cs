﻿// <auto-generated />
using Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Api.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "CD Projekt RED"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Konami"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Bungie"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Xbox Game Studios"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Perelesoq"
                        });
                });

            modelBuilder.Entity("Api.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DeveloperId")
                        .HasColumnType("integer");

                    b.Property<int>("PublisherId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DeveloperId = 1,
                            PublisherId = 1,
                            Title = "Cyberpunk 2077",
                            Year = 0
                        },
                        new
                        {
                            Id = 2,
                            DeveloperId = 2,
                            PublisherId = 2,
                            Title = "Metal Gear Solid",
                            Year = 0
                        },
                        new
                        {
                            Id = 3,
                            DeveloperId = 3,
                            PublisherId = 4,
                            Title = "Halo: Combat Evolved",
                            Year = 0
                        },
                        new
                        {
                            Id = 4,
                            DeveloperId = 5,
                            PublisherId = 5,
                            Title = "Torn Away",
                            Year = 0
                        });
                });

            modelBuilder.Entity("Api.Models.GameNote", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("PlayerId")
                        .HasColumnType("text");

                    b.Property<bool>("Completed")
                        .HasColumnType("boolean");

                    b.Property<bool>("Favourite")
                        .HasColumnType("boolean");

                    b.Property<bool>("PlayLater")
                        .HasColumnType("boolean");

                    b.HasKey("GameId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("GameNotes");

                    b.HasData(
                        new
                        {
                            GameId = 1,
                            PlayerId = "enhisir",
                            Completed = true,
                            Favourite = true,
                            PlayLater = true
                        },
                        new
                        {
                            GameId = 3,
                            PlayerId = "enhisir",
                            Completed = false,
                            Favourite = false,
                            PlayLater = true
                        });
                });

            modelBuilder.Entity("Api.Models.Player", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHashed")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("UserName");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            UserName = "enhisir",
                            PasswordHashed = "baPSLPhLKEISy/ig8xqmMQ==;5b3EjA2COq5gozxHPNiRhjWGtvdaXch7GiemoaN0NJQ=",
                            Role = 0
                        },
                        new
                        {
                            UserName = "nikoimam",
                            PasswordHashed = "lVneYZ/0x5AQox1i/MEP7w==;Ay51S2b0wrAk2WYCHAt2SVneyHAknRYW1dj/QwSms+4=",
                            Role = 0
                        });
                });

            modelBuilder.Entity("Api.Models.Game", b =>
                {
                    b.HasOne("Api.Models.Company", "Developer")
                        .WithMany()
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.Company", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("Api.Models.GameNote", b =>
                {
                    b.HasOne("Api.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });
#pragma warning restore 612, 618
        }
    }
}
