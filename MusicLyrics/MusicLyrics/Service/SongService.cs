using MusicLyrics.Data;
using MusicLyrics.Models;
using Microsoft.EntityFrameworkCore;
using MusicLyrics.Interfaces;

namespace MusicLyrics.Services
{
    public class SongService : ISongService
    {
        private readonly ApplicationDbContext _context;

        public SongService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SongDTO>> GetSongsAsync()
        {
            return await _context.Songs
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .Select(s => new SongDTO
                {
                    SongId = s.SongId,
                    Title = s.Title,
                    ArtistId = s.ArtistId,
                    AlbumId = s.AlbumId,
                    Genre = s.Genre,
                    ReleaseDate = s.ReleaseDate,
                    Lyrics = s.Lyrics
                })
                .ToListAsync();
        }

        public async Task<SongDTO> GetSongByIdAsync(int id)
        {
            var song = await _context.Songs
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .Where(s => s.SongId == id)
                .Select(s => new SongDTO
                {
                    SongId = s.SongId,
                    Title = s.Title,
                    ArtistId = s.ArtistId,
                    AlbumId = s.AlbumId,
                    Genre = s.Genre,
                    ReleaseDate = s.ReleaseDate,
                    Lyrics = s.Lyrics
                })
                .FirstOrDefaultAsync();

            return song;
        }

        public async Task<SongDTO> CreateSongAsync(SongDTO songDTO)
        {
            var song = new Song
            {
                Title = songDTO.Title,
                ArtistId = songDTO.ArtistId,
                AlbumId = songDTO.AlbumId,
                Genre = songDTO.Genre,
                ReleaseDate = songDTO.ReleaseDate,
                Lyrics = songDTO.Lyrics
            };

            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            songDTO.SongId = song.SongId;
            return songDTO;
        }

        public async Task UpdateSongAsync(int id, SongDTO songDTO)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
                throw new Exception("Song not found");

            song.Title = songDTO.Title;
            song.ArtistId = songDTO.ArtistId;
            song.AlbumId = songDTO.AlbumId;
            song.Genre = songDTO.Genre;
            song.ReleaseDate = songDTO.ReleaseDate;
            song.Lyrics = songDTO.Lyrics;

            _context.Entry(song).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSongAsync(int id)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
                throw new Exception("Song not found");

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
        }
    }
}
