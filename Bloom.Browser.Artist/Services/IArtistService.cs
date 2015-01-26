using System;

namespace Bloom.Browser.Artist.Services
{
    public interface IArtistService
    {
        void NewArtistTab();

        void DuplicateArtistTab(Guid tabId);
    }
}
