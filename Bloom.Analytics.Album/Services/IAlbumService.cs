using System;

namespace Bloom.Analytics.AlbumModule.Services
{
    public interface IAlbumService
    {
        void NewAlbumTab();

        void DuplicateAlbumTab(Guid tabId);
    }
}
