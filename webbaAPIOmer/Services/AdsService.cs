using Microsoft.EntityFrameworkCore;
using webbaAPIOmer.Data;
using webbaAPIOmer.Models;

namespace webbaAPIOmer.Services
{
    public class AdsService
    {
        private readonly AppDbContext _context;

        public AdsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ad>> GetAllAsync()
        {
            return await _context.Ads.ToListAsync();
        }

        public async Task<Ad?> GetByIdAsync(int id)
        {
            return await _context.Ads.FindAsync(id);
        }

        public async Task AddAsync(Ad ad)
        {
            _context.Ads.Add(ad);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ad ad)
        {
            _context.Entry(ad).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ad = await _context.Ads.FindAsync(id);
            if (ad != null)
            {
                _context.Ads.Remove(ad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
