using MusicLyrics.Models;

namespace MusicLyrics.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumDTO>> GetAlbumsAsync();
        Task<AlbumDTO> GetAlbumByIdAsync(int id);
        Task<AlbumDTO> CreateAlbumAsync(AlbumDTO albumDto);
        Task UpdateAlbumAsync(int id, AlbumDTO albumDto);
        Task DeleteAlbumAsync(int id);
    }
}
