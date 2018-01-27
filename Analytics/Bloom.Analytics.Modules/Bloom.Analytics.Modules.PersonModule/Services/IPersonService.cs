using System;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Analytics.Modules.PersonModule.Services
{
    /// <summary>
    /// Service for analytics person operations.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Creates a new person tab.
        /// </summary>
        /// <param name="personBuid">The person Bloom identifier.</param>
        void NewPersonTab(Buid personBuid);

        /// <summary>
        /// Restores the person tab.
        /// </summary>
        /// <param name="tab">The person tab.</param>
        void RestorePersonTab(Tab tab);

        /// <summary>
        /// Duplicates a person tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
        void DuplicatePersonTab(Guid tabId);
    }
}
