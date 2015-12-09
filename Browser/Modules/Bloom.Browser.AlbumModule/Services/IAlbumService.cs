using System;
using Bloom.State.Domain.Models;

namespace Bloom.Browser.AlbumModule.Services
{
    public interface IAlbumService
    {
        /// <summary>
        /// Creates a new album tab.
        /// </summary>
        /// <param name="albumId">The album identifier.</param>
        void NewAlbumTab(Guid albumId);

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
