using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicLyrics.Models
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }

        public required string Title { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        [ForeignKey("Album")]
        public int? AlbumId { get; set; }
        public Album Album { get; set; }

        public string Genre { get; set; }

        public DateTime? ReleaseDate { get; set; }
        
        public string Lyrics { get; set; }

       
    }
    public class SongDTO
    {
        public int SongId { get; set; }

        public required string Title { get; set; }

     
        public int ArtistId { get; set; }
        

        public int? AlbumId { get; set; }


        public string Genre { get; set; }
        public string Lyrics { get; set; }

        public DateTime? ReleaseDate { get; set; }

    }
}
