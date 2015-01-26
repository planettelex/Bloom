using System;

namespace Bloom.Browser.Playlist.Services
{
    public interface IPlaylistService
    {
        void NewPlaylistTab();

        void DuplicatePlaylistTab(Guid tabId);
    }
}
