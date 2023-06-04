using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            // добавляем контекст ApplicationContext в качестве сервиса в приложение
            builder.Services.AddDbContext<AppServerContext>(options => 
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            var app = builder.Build();

            // получение данных
            app.MapGet("/", (AppServerContext db) => db.Users.ToList());

            app.Run();
        }
    }
}