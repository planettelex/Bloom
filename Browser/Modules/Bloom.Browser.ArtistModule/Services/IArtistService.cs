using System;

namespace Bloom.Browser.ArtistModule.Services
{
    public interface IArtistService
    {
        void NewArtistTab(Guid artistId);

        void DuplicateArtistTab(Guid tabId);
    }
}
