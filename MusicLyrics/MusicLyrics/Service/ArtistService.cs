
using MusicLyrics.Data;
using MusicLyrics.Models;
using Microsoft.EntityFrameworkCore;
using MusicLyrics.Interfaces;

namespace MusicLyrics.Services
{
    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;

        public ArtistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArtistDTO>> GetArtistsAsync()
        {
            return await _context.Artist
                .Select(a => new ArtistDTO
                {
                    ArtistId = a.ArtistId,
                    Name = a.Name,
                    Bio = a.Bio,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<ArtistDTO> GetArtistByIdAsync(int id)
        {
            var artist = await _context.Artist
                .Where(a => a.ArtistId == id)
                .Select(a => new ArtistDTO
                {
                    ArtistId = a.ArtistId,
                    Name = a.Name,
                    Bio = a.Bio,
                    CreatedAt = a.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (artist == null)
                throw new KeyNotFoundException($"Artist with ID {id} not found.");

            return artist;
        }

        public async Task<ArtistDTO> CreateArtistAsync(ArtistDTO artistDTO)
        {
            var artist = new Artist
            {
                Name = artistDTO.Name,
                Bio = artistDTO.Bio,
                CreatedAt = artistDTO.CreatedAt
            };

            _context.Artist.Add(artist);
            await _context.SaveChangesAsync();

            artistDTO.ArtistId = artist.ArtistId;
            return artistDTO;
        }

        public async Task UpdateArtistAsync(int id, ArtistDTO artistDTO)
        {
            var artist = await _context.Artist.FindAsync(id);

            if (artist == null)
                throw new KeyNotFoundException($"Artist with ID {id} not found.");

            artist.Name = artistDTO.Name;
            artist.Bio = artistDTO.Bio;
            artist.CreatedAt = artistDTO.CreatedAt;

            _context.Entry(artist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteArtistAsync(int id)
        {
            var artist = await _context.Artist.FindAsync(id);

            if (artist == null)
                throw new KeyNotFoundException($"Artist with ID {id} not found.");

            _context.Artist.Remove(artist);
            await _context.SaveChangesAsync();
        }
    }
}