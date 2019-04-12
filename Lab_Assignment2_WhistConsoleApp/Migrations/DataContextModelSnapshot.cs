﻿// <auto-generated />
using System;
using Lab_Assignment2_WhistPointCalculator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lab_Assignment2_WhistConsoleApp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview3.19153.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lab_Assignment2_WhistConsoleApp.DATA.Team.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("Points");

                    b.HasKey("TeamId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Lab_Assignment2_WhistPointCalculator.GamePlayer", b =>
                {
                    b.Property<int>("GamePlayerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GamesId");

                    b.Property<int>("PlayerId");

                    b.Property<int>("PlayerPosition");

                    b.Property<int>("TeamId");

                    b.HasKey("GamePlayerId");

                    b.HasIndex("GamesId");

                    b.HasIndex("PlayerId");

                    b.ToTable("GamePlayers");
                });

            modelBuilder.Entity("Lab_Assignment2_WhistPointCalculator.GameRoundPlayers", b =>
                {
                    b.Property<int>("GameRoundPlayerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GamePlayerId");

                    b.Property<int>("GameRoundId");

                    b.Property<int>("Points");

                    b.HasKey("GameRoundPlayerId");

                    b.HasIndex("GamePlayerId");

                    b.HasIndex("GameRoundId");

                    b.ToTable("GameRoundPlayers");
                });

            modelBuilder.Entity("Lab_Assignment2_WhistPointCalculator.GameRounds", b =>
                {
                    b.Property<int>("GameRoundsId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DealerPosition");

                    b.Property<bool>("Ended");

                    b.Property<int>("GamesId");

                    b.Property<int>("RoundNumber");

                    b.Property<bool>("Started");

                    b.Property<string>("Trump");

                    b.HasKey("GameRoundsId");

                    b.HasIndex("GamesId");

                    b.ToTable("GameRounds");
                });

            modelBuilder.Entity("Lab_Assignment2_WhistPointCalculator.Games", b =>
                {
                    b.Property<int>("GamesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ended");

                    b.Property<int>("LocationId");

                    b.Property<string>("Name");

                    b.Property<bool>("Started");

                    b.Property<DateTime>("Updated");

                    b.HasKey("GamesId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Lab_Assignment2_WhistPointCalculator.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Lab_Assignment2_WhistPointCalculator.Players", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("PlayerId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Lab_Assignment2_WhistPointCalculator.GamePlayer", b =>
                {
                    b.HasOne("Lab_Assignment2_WhistConsoleApp.DATA.Team.Team", "Teams")
                        .WithMany("GamePlayers")
                        .HasForeignKey("GamePlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lab_Assignment2_WhistPointCalculator.Games", "Game")
                        .WithMany("GamePlayers")
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lab_Assignment2_WhistPointCalculator.Players", "Player")
                        .WithMany("GamePlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lab_Assignment2_WhistPointCalculator.GameRoundPlayers", b =>
                {
                    b.HasOne("Lab_Assignment2_WhistPointCalculator.GamePlayer", "GamePlayer")
                        .WithMany("GRPs")
                        .HasForeignKey("GamePlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lab_Assignment2_WhistPointCalculator.GameRounds", "GameRound")
                        .WithMany("GRPs")
                        .HasForeignKey("GameRoundId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Lab_Assignment2_WhistPointCalculator.GameRounds", b =>
                {
                    b.HasOne("Lab_Assignment2_WhistPointCalculator.Games", "Game")
                        .WithMany("GameRounds")
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lab_Assignment2_WhistPointCalculator.Location", b =>
                {
                    b.HasOne("Lab_Assignment2_WhistPointCalculator.Games", "Game")
                        .WithOne("Location")
                        .HasForeignKey("Lab_Assignment2_WhistPointCalculator.Location", "LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
