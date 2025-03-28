using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Chess.CHessGame
{
    public class ChessGame
    {
        public int Id { get; set; }
        public string? Moves { get; set; }
        public DateTime Date { get; set; }       
    }

    public class ChessDbContext : DbContext
    {
        public ChessDbContext(DbContextOptions<ChessDbContext> options) : base(options)
        {
        }

        public DbSet<ChessGame> PressReleases { get; set; }
    }
}
