using System;

namespace Bloom.Browser.PlaylistModule.Services
{
    public interface IPlaylistService
    {
        void NewPlaylistTab();

        void DuplicatePlaylistTab(Guid tabId);
    }
}
