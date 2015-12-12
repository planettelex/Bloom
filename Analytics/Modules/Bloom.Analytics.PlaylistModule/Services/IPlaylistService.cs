using System;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Analytics.PlaylistModule.Services
{
    public interface IPlaylistService
    {
        void NewPlaylistTab(Buid playlistBuid);

        void RestorePlaylistTab(Tab tab);

        void DuplicatePlaylistTab(Guid tabId);
    }
}
