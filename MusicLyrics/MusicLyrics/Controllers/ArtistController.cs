using Microsoft.AspNetCore.Mvc;
using MusicLyrics.Interfaces;
using MusicLyrics.Models;

namespace MusicLyrics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetArtists()
        {
            var artists = await _artistService.GetArtistsAsync();
            return Ok(artists);
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtist(int id)
        {
            var artist = await _artistService.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        // POST: api/Artists
        [HttpPost]
        public async Task<ActionResult<ArtistDTO>> PostArtist(ArtistDTO artistDTO)
        {
            var createdArtist = await _artistService.CreateArtistAsync(artistDTO);
            return CreatedAtAction(nameof(GetArtist), new { id = createdArtist.ArtistId }, createdArtist);
        }

        // PUT: api/Artists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, ArtistDTO artistDTO)
        {
            if (id != artistDTO.ArtistId)
            {
                return BadRequest();
            }

            try
            {
                await _artistService.UpdateArtistAsync(id, artistDTO);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            try
            {
                await _artistService.DeleteArtistAsync(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
