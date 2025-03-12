using System.ComponentModel.DataAnnotations;
namespace MusicLyrics.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }

        
        public required string Name { get; set; }

        public required string Bio { get; set; }

        public DateTime CreatedAt { get; set; }
        public ICollection<Album> Albums { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
    public class ArtistDTO
    {
        public int ArtistId { get; set; }


        public required string Name { get; set; }

        public required string Bio { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
