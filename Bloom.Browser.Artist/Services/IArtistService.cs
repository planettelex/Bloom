using System;

namespace Bloom.Browser.ArtistModule.Services
{
    public interface IArtistService
    {
        void NewArtistTab();

        void DuplicateArtistTab(Guid tabId);
    }
}
