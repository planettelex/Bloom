using System;

namespace Bloom.Analytics.Artist.Services
{
    public interface IArtistService
    {
        void NewArtistTab();

        void DuplicateArtistTab(Guid tabId);
    }
}
