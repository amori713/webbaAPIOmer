using Microsoft.EntityFrameworkCore;
using webbaAPIOmer.Models;  // Se till att du har rätt using

namespace webbaAPIOmer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Ad> Ads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definiera precision och skala för Price
            modelBuilder.Entity<Ad>()
                .Property(a => a.Price)
                .HasColumnType("decimal(18, 2)"); // Exempel: 18 siffror totalt, 2 siffror efter decimal
        }
    }
}
