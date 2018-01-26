using System;
using Bloom.Analytics.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Analytics.LibraryModule.Services
{
    /// <summary>
    /// Service for analytics library operations.
    /// </summary>
    public interface ILibraryService
    {
        /// <summary>
        /// Creates a new library tab.
        /// </summary>
        void NewLibraryTab(Guid libraryId);

        /// <summary>
        /// Restores the library tab.
        /// </summary>
        /// <param name="tab">The library tab.</param>
        void RestoreLibraryTab(Tab tab);

        /// <summary>
        /// Duplicates a library tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
        void DuplicateLibraryTab(Guid tabId);

        /// <summary>
        /// Changes a tab view.
        /// </summary>
        /// <param name="tabId">The tab identifier of the view.</param>
        /// <param name="viewType">The view type to change to.</param>
        void ChangeTabView(Guid tabId, ViewType viewType);
    }
}
