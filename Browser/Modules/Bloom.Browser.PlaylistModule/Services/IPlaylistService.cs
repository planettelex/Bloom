using System;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Browser.PlaylistModule.Services
{
    /// <summary>
    /// Service for browser playlist operations.
    /// </summary>
    public interface IPlaylistService
    {
        /// <summary>
        /// Creates a new playlist tab.
        /// </summary>
        /// <param name="playlistBuid">The playlist Bloom identifier.</param>
        void NewPlaylistTab(Buid playlistBuid);

        /// <summary>
        /// Restores the playlist tab.
        /// </summary>
        /// <param name="tab">The playlist tab.</param>
        void RestorePlaylistTab(Tab tab);

        /// <summary>
        /// Duplicates an playlist tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
        void DuplicatePlaylistTab(Guid tabId);
    }
}
