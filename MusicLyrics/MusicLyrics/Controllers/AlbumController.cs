using Microsoft.AspNetCore.Mvc;
using MusicLyrics.Interfaces;
using MusicLyrics.Models;

namespace MusicLyrics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        // GET: api/Album
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAlbums()
        {
            var albums = await _albumService.GetAlbumsAsync();
            return Ok(albums);
        }

        // GET: api/Album/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumDTO>> GetAlbum(int id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        // POST: api/Album
        [HttpPost]
        public async Task<ActionResult<AlbumDTO>> PostAlbum(AlbumDTO albumDto)
        {
            var createdAlbum = await _albumService.CreateAlbumAsync(albumDto);
            return CreatedAtAction(nameof(GetAlbum), new { id = createdAlbum.AlbumId }, createdAlbum);
        }

        // PUT: api/Album/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, AlbumDTO albumDto)
        {
            if (id != albumDto.AlbumId)
            {
                return BadRequest();
            }

            try
            {
                await _albumService.UpdateAlbumAsync(id, albumDto);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Album/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            try
            {
                await _albumService.DeleteAlbumAsync(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
