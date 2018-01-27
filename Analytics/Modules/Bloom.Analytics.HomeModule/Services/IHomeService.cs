using System;
using Bloom.State.Domain.Models;

namespace Bloom.Analytics.Modules.HomeModule.Services
{
    /// <summary>
    /// Service for analytics home and getting started page operations.
    /// </summary>
    public interface IHomeService
    {
        /// <summary>
        /// Creates a new home tab.
        /// </summary>
        void NewHomeTab();

        /// <summary>
        /// Restores the home tab.
        /// </summary>
        /// <param name="tab">The home tab.</param>
        void RestoreHomeTab(Tab tab);

        /// <summary>
        /// Duplicates the home tab.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        void DuplicateHomeTab(Guid tabId);

        /// <summary>
        /// Creates a new getting started tab.
        /// </summary>
        void NewGettingStartedTab();

        /// <summary>
        /// Restores the getting started tab.
        /// </summary>
        /// <param name="tab">The tab.</param>
        void RestoreGettingStartedTab(Tab tab);

        /// <summary>
        /// Duplicates the getting started tab.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        void DuplicateGettingStartedTab(Guid tabId);
    }
}
