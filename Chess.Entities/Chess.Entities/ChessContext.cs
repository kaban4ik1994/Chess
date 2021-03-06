﻿using Chess.Entities.Mapping;
using Chess.Entities.Models;
using Repository.Pattern.Ef6;
using System.Data.Entity;

namespace Chess.Entities
{
    public partial class ChessContext : DataContext
    {
        static ChessContext()
        {
            Database.SetInitializer<ChessContext>(new ChessContextInitializer());
        }

        public ChessContext()
            : base("ChessContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerGame> PlayerGames { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameLog> GameLogs { get; set; }
        public DbSet<Bot> Bots { get; set; }
        public DbSet<DebutGame> DebutGames { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new TokenMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new BotMap());
            modelBuilder.Configurations.Add(new GameLogMap());
            modelBuilder.Configurations.Add(new GameMap());
            modelBuilder.Configurations.Add(new InvitationMap());
            modelBuilder.Configurations.Add(new PlayerGameMap());
            modelBuilder.Configurations.Add(new PlayerMap());
            modelBuilder.Configurations.Add(new DebutGameMap());
        }
    }
}
