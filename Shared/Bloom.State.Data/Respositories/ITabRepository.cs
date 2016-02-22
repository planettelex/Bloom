using System;
using System.Collections.Generic;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Access methods for tab data.
    /// </summary>
    public interface ITabRepository
    {
        /// <summary>
        /// Gets the tab.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        Tab GetTab(Guid tabId);

        /// <summary>
        /// Lists the tabs.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <param name="userId">The user identifier.</param>
        List<Tab> ListTabs(ProcessType process, Guid userId);

        /// <summary>
        /// Adds a tab.
        /// </summary>
        /// <param name="tab">The tab.</param>
        void AddTab(Tab tab);

        /// <summary>
        /// Deletes a tab.
        /// </summary>
        /// <param name="tab">The tab.</param>
        void DeleteTab(Tab tab);
    }
}
