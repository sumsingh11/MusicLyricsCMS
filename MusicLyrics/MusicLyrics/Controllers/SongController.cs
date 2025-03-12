using Microsoft.AspNetCore.Mvc;
using MusicLyrics.Interfaces;
using MusicLyrics.Models;

namespace MusicLyrics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetSongs()
        {
            var songs = await _songService.GetSongsAsync();
            return Ok(songs);
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SongDTO>> GetSong(int id)
        {
            var song = await _songService.GetSongByIdAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }

        // POST: api/Songs
        [HttpPost]
        public async Task<ActionResult<SongDTO>> PostSong(SongDTO songDTO)
        {
            var createdSong = await _songService.CreateSongAsync(songDTO);
            return CreatedAtAction(nameof(GetSong), new { id = createdSong.SongId }, createdSong);
        }

        // PUT: api/Songs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, SongDTO songDTO)
        {
            if (id != songDTO.SongId)
            {
                return BadRequest();
            }

            try
            {
                await _songService.UpdateSongAsync(id, songDTO);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            try
            {
                await _songService.DeleteSongAsync(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
