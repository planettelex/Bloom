using System;
using Bloom.Browser.Common;
using Bloom.PubSubEvents.EventModels;
using Bloom.State.Domain.Models;

namespace Bloom.Browser.Modules.LibraryModule.Services
{
    /// <summary>
    /// Service for browser library operations.
    /// </summary>
    public interface ILibraryService
    {
        /// <summary>
        /// Shows the create new library modal window.
        /// </summary>
        void ShowCreateNewLibraryModal();
        
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
        /// Shows the add music modal window.
        /// </summary>
        void ShowAddMusicModal();

        /// <summary>
        /// Creates a new add music tab.
        /// </summary>
        /// <param name="eventModel">The event model.</param>
        void NewAddMusicTab(AddMusicEventModel eventModel);

        /// <summary>
        /// Restores the add music tab.
        /// </summary>
        /// <param name="tab">The add music tab.</param>
        void RestoreAddMusicTab(Tab tab);

        /// <summary>
        /// Duplicates an add music tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
        void DuplicateAddMusicTab(Guid tabId);

        /// <summary>
        /// Changes a tab view.
        /// </summary>
        /// <param name="tabId">The tab identifier of the view.</param>
        /// <param name="viewType">The view type to change to.</param>
        void ChangeTabView(Guid tabId, ViewType viewType);
    }
}
