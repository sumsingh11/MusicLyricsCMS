using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicLyrics.Interfaces;
using MusicLyrics.Models;
using MusicLyrics.Services;

namespace MusicLyrics.Controllers
{
    public class SongPageController : Controller
    {
        private readonly ISongService _songService;
        private readonly IArtistService _artistService;
        private readonly IAlbumService _albumService;

        public SongPageController(ISongService songService, IArtistService artistService, IAlbumService albumService)
        {
            _songService = songService;
            _artistService = artistService;
            _albumService = albumService;
        }

        // GET: SongPage
        public async Task<IActionResult> Index()
        {
            var songs = await _songService.GetSongsAsync();
            return View(songs);
        }

        // GET: SongPage/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var song = await _songService.GetSongByIdAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            // Fetch Artist and Album details
            var artist = song.ArtistId > 0
                ? await _artistService.GetArtistByIdAsync(song.ArtistId)
                : null;

            var album = song.AlbumId.HasValue
        ? await _albumService.GetAlbumByIdAsync(song.AlbumId.Value)
        : null;

            // Create ViewModel with song details
            var viewModel = new SongViewModel
            {
                Song = song,
                ArtistName = artist?.Name ?? "Unknown Artist",
                AlbumTitle = album?.Title ?? "Unknown Album"
            };

            return View(viewModel);
        }

        // GET: SongPage/Create
        public async Task<IActionResult> Create()
        {
            var artists = await _artistService.GetArtistsAsync();
            var albums = await _albumService.GetAlbumsAsync();

            var viewModel = new SongViewModel
            {
                Artists = artists.Select(a => new SelectListItem
                {
                    Value = a.ArtistId.ToString(),
                    Text = a.Name
                }).ToList(),

                Albums = albums.Select(a => new SelectListItem
                {
                    Value = a.AlbumId.ToString(),
                    Text = a.Title
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SongViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _songService.CreateSongAsync(viewModel.Song);
                return RedirectToAction(nameof(Index));
            }

            // Reload dropdowns if ModelState is invalid
            var artists = await _artistService.GetArtistsAsync();
            var albums = await _albumService.GetAlbumsAsync();

            viewModel.Artists = artists.Select(a => new SelectListItem { Value = a.ArtistId.ToString(), Text = a.Name }).ToList();
            viewModel.Albums = albums.Select(a => new SelectListItem { Value = a.AlbumId.ToString(), Text = a.Title }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var song = await _songService.GetSongByIdAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            var artists = await _artistService.GetArtistsAsync();
            var albums = await _albumService.GetAlbumsAsync();

            var viewModel = new SongViewModel
            {
                Song = song,
                Artists = artists.Select(a => new SelectListItem
                {
                    Value = a.ArtistId.ToString(),
                    Text = a.Name,
                    Selected = (a.ArtistId == song.ArtistId)
                }).ToList(),

                Albums = albums.Select(a => new SelectListItem
                {
                    Value = a.AlbumId.ToString(),
                    Text = a.Title,
                    Selected = (a.AlbumId == song.AlbumId)
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SongViewModel viewModel)
        {
            if (id != viewModel.Song.SongId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _songService.UpdateSongAsync(id, viewModel.Song);
                return RedirectToAction(nameof(Index));
            }

            // Reload dropdowns if ModelState is invalid
            var artists = await _artistService.GetArtistsAsync();
            var albums = await _albumService.GetAlbumsAsync();

            viewModel.Artists = artists.Select(a => new SelectListItem { Value = a.ArtistId.ToString(), Text = a.Name }).ToList();
            viewModel.Albums = albums.Select(a => new SelectListItem { Value = a.AlbumId.ToString(), Text = a.Title }).ToList();

            return View(viewModel);
        }



        // GET: SongPage/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var song = await _songService.GetSongByIdAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: SongPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _songService.DeleteSongAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
