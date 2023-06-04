using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            // ��������� �������� ApplicationContext � �������� ������� � ����������
            builder.Services.AddDbContext<AppServerContext>(options => 
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            var app = builder.Build();

            // ��������� ������
            app.MapGet("/", (AppServerContext db) => db.Users.ToList());

            app.Run();
        }
    }
}