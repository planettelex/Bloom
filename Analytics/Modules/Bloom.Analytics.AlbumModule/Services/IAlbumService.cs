using System;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Analytics.Modules.AlbumModule.Services
{
    /// <summary>
    /// Service for analytics album operations.
    /// </summary>
    public interface IAlbumService
    {
        /// <summary>
        /// Creates a new album tab.
        /// </summary>
        /// <param name="albumBuid">The album Bloom identifier.</param>
        void NewAlbumTab(Buid albumBuid);

        /// <summary>
        /// Restores the album tab.
        /// </summary>
        /// <param name="tab">The album tab.</param>
        void RestoreAlbumTab(Tab tab);

        /// <summary>
        /// Duplicates an album tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
        void DuplicateAlbumTab(Guid tabId);
    }
}
