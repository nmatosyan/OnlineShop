using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Chess.CHessGame
{
    public class ChessGameDb
    {
        public int Id { get; set; }
        public string? Moves { get; set; }
        public DateTime Date { get; set; }       
    }

    public class ChessDbContext : DbContext
    {
        public DbSet<ChessGameDb> ChessGames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        }
    }
}
