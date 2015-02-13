using System;

namespace Bloom.Browser.AlbumModule.Services
{
    public interface IAlbumService
    {
        void NewAlbumTab();

        void DuplicateAlbumTab(Guid tabId);
    }
}
