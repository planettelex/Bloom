using System;

namespace Bloom.Analytics.AlbumModule.Services
{
    public interface IAlbumService
    {
        void NewAlbumTab(Guid albumId);

        void DuplicateAlbumTab(Guid tabId);
    }
}
