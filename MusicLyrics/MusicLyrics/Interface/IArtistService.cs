using MusicLyrics.Models;

namespace MusicLyrics.Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistDTO>> GetArtistsAsync();
        Task<ArtistDTO> GetArtistByIdAsync(int id);
        Task<ArtistDTO> CreateArtistAsync(ArtistDTO artistDTO);
        Task UpdateArtistAsync(int id, ArtistDTO artistDTO);
        Task DeleteArtistAsync(int id);
    }
}
