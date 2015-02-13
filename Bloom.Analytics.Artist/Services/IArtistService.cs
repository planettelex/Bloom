using System;

namespace Bloom.Analytics.ArtistModule.Services
{
    public interface IArtistService
    {
        void NewArtistTab();

        void DuplicateArtistTab(Guid tabId);
    }
}
