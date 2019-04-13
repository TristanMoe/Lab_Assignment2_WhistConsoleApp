using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Lab_Assignment2_WhistConsoleApp.DATA.Team;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Lab_Assignment2_WhistPointCalculator
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            
        }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=WhistDatabase;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }
        #region Entity Declaration
        public DbSet<GamePlayer> GamePlayers { get; set; }
        public DbSet<Players> Players { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<GameRounds> GameRounds { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<GameRoundPlayers> GameRoundPlayers { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*Missing hashkeys and Deletebehavior 
            Not sure if fluent foreign keys are necessary,
            when data annotation is used. 
            Missing a lot of constraints 
            HOWEVER it seems to add Cascade deletion automatically with items with no primary key
                - When primary key is another entitites' primary key. 
             */

            #region Keys 
            modelBuilder.Entity<Players>()
                .HasKey(p => p.PlayerId);

            modelBuilder.Entity<Games>()
                .HasKey(g => g.GamesId);

            modelBuilder.Entity<GameRounds>()
                .HasKey(p => p.GameRoundsId);

            modelBuilder.Entity<Location>()
                .HasKey(p => p.LocationId);

            modelBuilder.Entity<GamePlayer>()
                .HasKey(k => k.GamePlayerId);

            modelBuilder.Entity<GameRoundPlayers>()
                .HasKey(k => k.GameRoundPlayerId);

            modelBuilder.Entity<Team>()
                .HasKey(k => k.TeamId);

            #endregion
            modelBuilder.Entity<Players>()
                .Property(p => p.PlayerId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Games>()
                .Property(g => g.GamesId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<GameRounds>()
                .Property(p => p.GameRoundsId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Location>()
                .Property(p => p.LocationId)
                .ValueGeneratedOnAdd();

            
            modelBuilder.Entity<GamePlayer>()
                .Property(k => k.GamePlayerId)
                .ValueGeneratedOnAdd();
                
            modelBuilder.Entity<GameRoundPlayers>()
                .Property(k => k.GameRoundPlayerId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Team>()
                .Property(k => k.TeamId)
                .ValueGeneratedOnAdd();
            

            #region Games & Relations
            // <summary>
            // One location has one Game, but a Game
            // can be placed in several locations
            // </summary>
            modelBuilder.Entity<Games>()
                .HasOne(g => g.Location)
                .WithOne(l => l.Game)
                .HasForeignKey<Games>(g => g.LocationId);

            // <summary>
            // A Game can have several players 
            // </summary>
            modelBuilder.Entity<Games>()
                .HasMany(g => g.GamePlayers)
                .WithOne(gp => gp.Game)
                .HasForeignKey(gp => gp.GamesId); 

            // <summary>
            // A Game can have several Gamerounds
            // </summary>
            modelBuilder.Entity<GameRounds>()
                .HasOne(gr => gr.Game)
                .WithMany(g => g.GameRounds)
                .HasForeignKey(g => g.GamesId); 
            #endregion

           

            
            // <summary>
            // A Game Round can consists of several rounds
            // </summary>
         

            modelBuilder.Entity<GameRoundPlayers>()
                .HasOne(gr => gr.GameRound)
                .WithMany(grp => grp.GRPs)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(grp => grp.GameRoundId);
            
           

            // <summary>
            // Player has one Gameplayer,
            // but GamePlayer has many players?
            // </summary>
            #region Players & GamePlayers
            modelBuilder.Entity<GamePlayer>()
                .HasOne(p => p.Player)
                .WithMany(gp => gp.GamePlayers)
                .HasForeignKey(gm => gm.PlayerId);
            #endregion

            #region PlayerPosition Relations 
            

            modelBuilder.Entity<GameRoundPlayers>()
                .HasOne(gp => gp.GamePlayer)
                .WithMany(grp => grp.GRPs)
                .HasForeignKey(grp => grp.GamePlayerId);
            #endregion

            modelBuilder.Entity<GamePlayer>()
                .HasOne(gr => gr.Teams)
                .WithMany(g => g.GamePlayers)
                .HasForeignKey(g => g.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
                

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Games)
                .WithMany(g => g.Teams)
                .HasForeignKey(g => g.GamesId)
                .OnDelete(DeleteBehavior.Restrict);

            //#region DataSeeding
            //modelBuilder.Entity<Games>().HasData(
            //    new Games() { Ended = false, GamesId = 1, LocationId = 1, Name = "SuperWeebTanks", Started = true, Updated = DateTime.Now });
            //modelBuilder.Entity<GameRounds>().HasData(
            //    new GameRounds() { DealerPosition = 1, Ended = false, GameRoundsId = 1, GamesId = 1, RoundNumber = 1, Started = true });
            //modelBuilder.Entity<GameRoundPlayers>().HasData(
            //    new GameRoundPlayers() { GamePlayerId = 1, GameRoundPlayerId = 1, GameRoundId = 1, Points = 5 });
            //modelBuilder.Entity<GameRoundPlayers>().HasData(
            //    new GameRoundPlayers() { GamePlayerId = 2, GameRoundPlayerId = 2, GameRoundId = 1, Points = 3 });
            //modelBuilder.Entity<GameRoundPlayers>().HasData(
            //    new GameRoundPlayers() { GamePlayerId = 3, GameRoundPlayerId = 3, GameRoundId = 1, Points = 2 });
            //modelBuilder.Entity<GameRoundPlayers>().HasData(
            //    new GameRoundPlayers() { GamePlayerId = 4, GameRoundPlayerId = 4, GameRoundId = 1, Points = 3 });
            //modelBuilder.Entity<GamePlayer>().HasData(
            //    new GamePlayer() { GamePlayerId = 1, GamesId = 1, PlayerId = 1, PlayerPosition = 1, TeamId = 1 });
            //modelBuilder.Entity<GamePlayer>().HasData(
            //    new GamePlayer() { GamePlayerId = 2, GamesId = 1, PlayerId = 2, PlayerPosition = 2, TeamId = 1 });
            //modelBuilder.Entity<GamePlayer>().HasData(
            //    new GamePlayer() { GamePlayerId = 3, GamesId = 1, PlayerId = 3, PlayerPosition = 3, TeamId = 2 });
            //modelBuilder.Entity<GamePlayer>().HasData(
            //    new GamePlayer() { GamePlayerId = 4, GamesId = 1, PlayerId = 4, PlayerPosition = 4, TeamId = 2 });
            //modelBuilder.Entity<Players>().HasData(
            //    new Players() { FirstName = "Tristan", LastName = "Moller", PlayerId = 1 });
            //modelBuilder.Entity<Players>().HasData(
            //    new Players() { FirstName = "Martin", LastName = "Jespersen", PlayerId = 2 });
            //modelBuilder.Entity<Players>().HasData(
            //    new Players() { FirstName = "Marcus", LastName = "Gasberg", PlayerId = 3 });
            //modelBuilder.Entity<Players>().HasData(
            //    new Players() { FirstName = "Mathias", LastName = "Hansen", PlayerId = 4 });
            //modelBuilder.Entity<Team>().HasData(
            //    new Team() { Name = "TheJedis", Points = 2, TeamId = 1 });
            //modelBuilder.Entity<Team>().HasData(
            //    new Team() { Name = "Memers", Points = 3, TeamId = 2 });
            //modelBuilder.Entity<Location>().HasData(
            //    new Location() { LocationId = 1, Name = "Kælderen" });
            //#endregion


        }
    }
}
