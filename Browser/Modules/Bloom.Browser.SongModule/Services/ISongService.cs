using System;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Browser.SongModule.Services
{
    /// <summary>
    /// Service for browser song operations.
    /// </summary>
    public interface ISongService
    {
        /// <summary>
        /// Creates a new song tab.
        /// </summary>
        /// <param name="songBuid">The song Bloom identifier.</param>
        void NewSongTab(Buid songBuid);

        /// <summary>
        /// Restores the song tab.
        /// </summary>
        /// <param name="tab">The song tab.</param>
        void RestoreSongTab(Tab tab);

        /// <summary>
        /// Duplicates a song tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
        void DuplicateSongTab(Guid tabId);
    }
}
