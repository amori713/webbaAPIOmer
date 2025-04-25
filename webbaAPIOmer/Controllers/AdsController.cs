using webbaAPIOmer.Models;
using Microsoft.AspNetCore.Mvc;
using webbaAPIOmer.Services;


namespace webbaAPIOmer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly AdsService _adsService;

        public AdsController(AdsService adsService)
        {
            _adsService = adsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ad>>> GetAds()
        {
            var ads = await _adsService.GetAllAsync();
            return Ok(ads);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ad>> GetAd(int id)
        {
            var ad = await _adsService.GetByIdAsync(id);
            return ad == null ? NotFound() : Ok(ad);
        }

        [HttpPost]
        public async Task<ActionResult<Ad>> CreateAd(Ad ad)
        {
            await _adsService.AddAsync(ad);
            return CreatedAtAction(nameof(GetAd), new { id = ad.Id }, ad);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAd(int id, Ad ad)
        {
            if (id != ad.Id)
                return BadRequest();

            await _adsService.UpdateAsync(ad);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAd(int id)
        {
            await _adsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
