using webbaAPIOmer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using webbaAPIOmer.Data;
// ?? Lägg till detta för att registrera din service

namespace webbaAPIOmer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ?? Lägg till DbContext (EF Core + Connection String)
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ? Registrera din service (DI = Dependency Injection)
            builder.Services.AddScoped<AdsService>();

            // ? Lägg till Controllers
            builder.Services.AddControllers();

            // ? Lägg till Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ? Aktivera Swagger direkt när appen startar
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ads API v1");
                options.RoutePrefix = string.Empty; // Swagger startar på root
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
