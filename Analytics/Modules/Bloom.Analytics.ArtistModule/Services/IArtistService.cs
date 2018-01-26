using System;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Analytics.ArtistModule.Services
{
    /// <summary>
    /// Service for analytics artist operations.
    /// </summary>
    public interface IArtistService
    {
        /// <summary>
        /// Creates a new artist tab.
        /// </summary>
        /// <param name="artistBuid">The artist Bloom identifier.</param>
        void NewArtistTab(Buid artistBuid);

        /// <summary>
        /// Restores the artist tab.
        /// </summary>
        /// <param name="tab">The artist tab.</param>
        void RestoreArtistTab(Tab tab);

        /// <summary>
        /// Duplicates an artist tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
        void DuplicateArtistTab(Guid tabId);
    }
}
