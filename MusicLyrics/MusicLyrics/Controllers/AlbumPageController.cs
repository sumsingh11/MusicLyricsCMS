using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicLyrics.Interfaces;
using MusicLyrics.Models;
using MusicLyrics.Services;

namespace MusicLyrics.Controllers
{
    public class AlbumPageController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IArtistService _artistService;

        public AlbumPageController(IAlbumService albumService,IArtistService artistService)
        {
            _albumService = albumService;
            _artistService = artistService;
        }

        // GET: AlbumPage
        public async Task<IActionResult> Index()
        {
            var albums = await _albumService.GetAlbumsAsync();
            return View(albums);
        }

        // GET: AlbumPage/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: AlbumPage/Create
        public async Task<IActionResult> Create()
        {
            var artists = await _artistService.GetArtistsAsync(); // Fetch artists from service
            var viewModel = new AlbumViewModel
            {
                Artists = artists.Select(a => new SelectListItem
                {
                    Value = a.ArtistId.ToString(),
                    Text = a.Name
                }).ToList()
            };
            return View(viewModel);
        }



        // POST: AlbumPage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlbumViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _albumService.CreateAlbumAsync(model.Album);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // GET: AlbumPage/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: AlbumPage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AlbumDTO albumDto)
        {
            if (id != albumDto.AlbumId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _albumService.UpdateAlbumAsync(id, albumDto);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(albumDto);
        }

        // GET: AlbumPage/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: AlbumPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _albumService.DeleteAlbumAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
