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

            app.MapGet("/data/read", async (AppServerContext db) =>
            {
                try
                {
                    List<Data>? data = await db.Datas.ToListAsync();
                    return Results.Json(data);
                }
                catch
                {
                    return Results.BadRequest();
                }
            });

            app.MapPost("/data/write", async (HttpRequest request, AppServerContext db) =>
            {
                try
                {
                    var objJson = await request.ReadFromJsonAsync<ValueData>();
                    if (objJson != null)
                    {
                        await db.Datas.AddAsync(new Data { Value = objJson.val });
                        await db.SaveChangesAsync();
                        return Results.Ok();
                    }
                    else
                    {
                        return Results.BadRequest();
                    }
                }
                catch
                {
                    return Results.BadRequest();
                }
            });

            app.Run();
        }
    }
}