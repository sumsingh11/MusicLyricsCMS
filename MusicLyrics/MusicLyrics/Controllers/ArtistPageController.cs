
using Microsoft.AspNetCore.Mvc;
using MusicLyrics.Interfaces;
using MusicLyrics.Models;

namespace MusicLyrics.Controllers
{
    public class ArtistPageController : Controller
    {
        private readonly IArtistService _artistService;

        public ArtistPageController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        public async Task<IActionResult> Index()
        {
            var artists = await _artistService.GetArtistsAsync();
            return View(artists);
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var artist = await _artistService.GetArtistByIdAsync(id);
                return View(artist);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtistDTO artistDTO)
        {
            if (ModelState.IsValid)
            {
                await _artistService.CreateArtistAsync(artistDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(artistDTO);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var artist = await _artistService.GetArtistByIdAsync(id);
                return View(artist);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArtistDTO artistDTO)
        {
            if (id != artistDTO.ArtistId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _artistService.UpdateArtistAsync(id, artistDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(artistDTO);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var artist = await _artistService.GetArtistByIdAsync(id);
                return View(artist);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _artistService.DeleteArtistAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}