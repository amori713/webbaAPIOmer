using webbaAPIOmer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using webbaAPIOmer.Data;

namespace webbaAPIOmer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // L�gg till DbContext (EF Core + Connection String)
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Registrera din service (DI = Dependency Injection)
            builder.Services.AddScoped<AdsService>();

            // L�gg till Controllers
            builder.Services.AddControllers();

            // L�gg till Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Till�mpa migrationer automatiskt n�r applikationen startar
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();  // K�r alla migrationer om de inte har k�rts �nnu
            }

            // Aktivera Swagger direkt n�r appen startar
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ads API v1");
                options.RoutePrefix = string.Empty; // Swagger startar p� root
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
