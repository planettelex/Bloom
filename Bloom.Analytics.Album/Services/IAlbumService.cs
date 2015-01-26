using System;

namespace Bloom.Analytics.Album.Services
{
    public interface IAlbumService
    {
        void NewAlbumTab();

        void DuplicateAlbumTab(Guid tabId);
    }
}
