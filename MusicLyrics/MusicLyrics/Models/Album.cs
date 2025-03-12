using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MusicLyrics.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }

        
        public required string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string CoverImage { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
    public class AlbumDTO
    {
        public int AlbumId { get; set; }
        public required string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string CoverImage { get; set; }

        public int ArtistId { get; set; }
        
    }
}
