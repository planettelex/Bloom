using System;

namespace Bloom.Analytics.PlaylistModule.Services
{
    public interface IPlaylistService
    {
        void NewPlaylistTab(Guid playlistId);

        void DuplicatePlaylistTab(Guid tabId);
    }
}
