using System;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Analytics.AlbumModule.Services
{
    public interface IAlbumService
    {
        void NewAlbumTab(Buid albumBuid);

        void RestoreAlbumTab(Tab tab);

        void DuplicateAlbumTab(Guid tabId);
    }
}
