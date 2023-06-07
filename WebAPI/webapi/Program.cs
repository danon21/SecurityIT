using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPI;

var builder = WebApplication.CreateBuilder();

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

app.Run();
