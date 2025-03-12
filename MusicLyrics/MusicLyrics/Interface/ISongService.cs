using MusicLyrics.Models;

namespace MusicLyrics.Interfaces
{
    public interface ISongService
    {
        Task<IEnumerable<SongDTO>> GetSongsAsync();
        Task<SongDTO> GetSongByIdAsync(int id);
        Task<SongDTO> CreateSongAsync(SongDTO songDTO);
        Task UpdateSongAsync(int id, SongDTO songDTO);
        Task DeleteSongAsync(int id);
    }
}
