using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.CHessGame;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Chess.ChessGame
{
    public class DotNetPagingDbContextFactory : IDesignTimeDbContextFactory<ChessDbContext>
    {
        private static string? _connectionString;

        public ChessDbContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public ChessDbContext CreateDbContext(string[]? args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<ChessDbContext>();
            builder.UseNpgsql(_connectionString);

            return new ChessDbContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
