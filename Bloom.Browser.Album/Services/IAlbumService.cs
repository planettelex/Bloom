using System;

namespace Bloom.Browser.Album.Services
{
    public interface IAlbumService
    {
        void NewAlbumTab();

        void DuplicateAlbumTab(Guid tabId);
    }
}
