using MusicLyrics.Data;
using MusicLyrics.Models;
using Microsoft.EntityFrameworkCore;
using MusicLyrics.Interfaces;

namespace MusicLyrics.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly ApplicationDbContext _context;

        public AlbumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlbumDTO>> GetAlbumsAsync()
        {
            return await _context.Albums
                .Select(a => new AlbumDTO
                {
                    AlbumId = a.AlbumId,
                    Title = a.Title,
                    ReleaseDate = a.ReleaseDate,
                    CoverImage = a.CoverImage,
                    ArtistId = a.ArtistId
                })
                .ToListAsync();
        }

        public async Task<AlbumDTO> GetAlbumByIdAsync(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
                return null;

            return new AlbumDTO
            {
                AlbumId = album.AlbumId,
                Title = album.Title,
                ReleaseDate = album.ReleaseDate,
                CoverImage = album.CoverImage,
                ArtistId = album.ArtistId
            };
        }

        public async Task<AlbumDTO> CreateAlbumAsync(AlbumDTO albumDto)
        {
            var album = new Album
            {
                Title = albumDto.Title,
                ReleaseDate = albumDto.ReleaseDate,
                CoverImage = albumDto.CoverImage,
                ArtistId = albumDto.ArtistId
            };

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            return albumDto;
        }

        public async Task UpdateAlbumAsync(int id, AlbumDTO albumDto)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
                throw new Exception("Album not found");

            album.Title = albumDto.Title;
            album.ReleaseDate = albumDto.ReleaseDate;
            album.CoverImage = albumDto.CoverImage;
            album.ArtistId = albumDto.ArtistId;

            _context.Entry(album).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlbumAsync(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
                throw new Exception("Album not found");

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
        }
    }
}
