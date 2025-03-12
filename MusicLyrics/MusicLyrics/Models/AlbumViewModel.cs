using Microsoft.AspNetCore.Mvc.Rendering;
using MusicLyrics.Models;
namespace MusicLyrics.Models
{
    public class AlbumViewModel
    {
        public AlbumDTO Album { get; set; }
        public List<SelectListItem> Artists { get; set; } = new();
    }
}