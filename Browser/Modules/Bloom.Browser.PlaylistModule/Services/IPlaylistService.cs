using System;

namespace Bloom.Browser.PlaylistModule.Services
{
    public interface IPlaylistService
    {
        void NewPlaylistTab(Guid playlistId);

        void DuplicatePlaylistTab(Guid tabId);
    }
}
