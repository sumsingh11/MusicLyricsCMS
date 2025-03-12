using Microsoft.AspNetCore.Mvc.Rendering;
using MusicLyrics.Models;
using System.Collections.Generic;

namespace MusicLyrics.Models
{
    public class SongViewModel
    {
        public SongDTO Song { get; set; } 

        // Dropdown lists for Create/Edit
        public List<SelectListItem> Artists { get; set; } = new();
        public List<SelectListItem> Albums { get; set; } = new();

        // Display Names for Details View
        public string? ArtistName { get; set; }
        public string? AlbumTitle { get; set; }
    }
}
