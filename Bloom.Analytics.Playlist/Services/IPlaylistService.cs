using System;

namespace Bloom.Analytics.Playlist.Services
{
    public interface IPlaylistService
    {
        void NewPlaylistTab();

        void DuplicatePlaylistTab(Guid tabId);
    }
}
