
using Microsoft.EntityFrameworkCore;

namespace OnlineShop;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. Настройка строки подключения
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // 2. Регистрация DbContext с использованием PostgreSQL
        builder.Services.AddDbContext<StoreDbContext>(options =>
            options.UseNpgsql(connectionString));

        // 3. Добавление сервисов контроллеров
        builder.Services.AddControllers();

        // 4. Добавление Swagger для документирования API
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // 5. Настройка CORS (по желанию)
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        // 6. Использование Swagger (в режиме разработки)
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // 7. Включение CORS
        app.UseCors("AllowAll");

        // 8. Маршрутизация запросов
        app.UseAuthorization();
        app.MapControllers();

        // 9. Запуск приложения
        app.Run();
    }
}
