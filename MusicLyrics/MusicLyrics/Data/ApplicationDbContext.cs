using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicLyrics.Models;

namespace MusicLyrics.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Artist>Artist {  get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
