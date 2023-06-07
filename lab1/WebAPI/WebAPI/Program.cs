using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPI;

var builder = WebApplication.CreateBuilder();

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<AppServerContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

/*// получение данных
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
*/
app.Run();