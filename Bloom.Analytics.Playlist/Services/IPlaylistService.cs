using System;

namespace Bloom.Analytics.PlaylistModule.Services
{
    public interface IPlaylistService
    {
        void NewPlaylistTab();

        void DuplicatePlaylistTab(Guid tabId);
    }
}
