using System;

namespace Bloom.Analytics.ArtistModule.Services
{
    public interface IArtistService
    {
        void NewArtistTab(Guid artistId);

        void DuplicateArtistTab(Guid tabId);
    }
}
