using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace Chess.CHessGame;

public class ChessGameDb
{
    public int Id { get; set; }
    public string? Moves { get; set; }
    public DateTime Date { get; set; }

    public int WhitePlayerId { get; set; }
    public User? WhitePlayer { get; set; }

    public int BlackPlayerId { get; set; }
    public User? BlackPlayer { get; set; }

    public List<MoveDb> MoveList { get; set; } = new();
}

public class MoveDb
{
    public int Id { get; set; }
    public string? Move { get; set; } = string.Empty;
    public int MoveNumber { get; set; }

    public int GameId { get; set; }
    public ChessGameDb Game { get; set; }
}

public class User
{
    public int UserId { get; set; }
    public string? Name { get; set; }

    public List<ChessGameDb> GamesAsWhite { get; set; } = new();
    public List<ChessGameDb> GamesAsBlack { get; set; } = new();
}

public class ChessDbContext : DbContext
{
    public DbSet<ChessGameDb> ChessGames { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<MoveDb> Moves { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChessGameDb>()
            .HasOne(g => g.WhitePlayer)
            .WithMany(u => u.GamesAsWhite)
            .HasForeignKey(g => g.WhitePlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ChessGameDb>()
            .HasOne(g => g.BlackPlayer)
            .WithMany(u => u.GamesAsBlack)
            .HasForeignKey(g => g.BlackPlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MoveDb>()
        .HasOne(m => m.Game)
        .WithMany(g => g.MoveList)
        .HasForeignKey(m => m.GameId);
    }
}
