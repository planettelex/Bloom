using System;
using System.Collections.Generic;
using System.Linq;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// Represents the state of a tabbed application.
    /// </summary>
    public abstract class TabbedApplicationState : ApplicationState
    {
        /// <summary>
        /// Gets or sets the tabs.
        /// </summary>
        public List<Tab> Tabs { get; set; }

        /// <summary>
        /// Gets the next tab order.
        /// </summary>
        /// <returns>The next tab order.</returns>
        public int GetNextTabOrder()
        {
            if (!HasTabs())
                return 1;

            var tabs = Tabs.OrderBy(tab => tab.Order).ToList();
            var lastTab = tabs[tabs.Count - 1];
            return lastTab.Order + 1;
        }

        /// <summary>
        /// Condenses the tab orders so each is a consecutive integer.
        /// </summary>
        public void CondenseTabOrders()
        {
            if (!HasTabs())
                return;

            for (var i = 0; i < Tabs.Count; i++)
                Tabs[i].Order = i + 1;
        }

        /// <summary>
        /// Determines whether there are any tabs.
        /// </summary>
        /// <returns></returns>
        public bool HasTabs()
        {
            return Tabs != null && Tabs.Any();
        }

        /// <summary>
        /// Determines whether there are any tabs connected to the specified library.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        public bool HasLibraryTabs(Guid libraryId)
        {
            if (Tabs == null)
                return false;

            return Tabs.Any(tab => tab.LibraryId == libraryId || (tab.Libraries != null && tab.Libraries.Any(tabLibrary => tabLibrary.LibraryId == libraryId)));
        }
    }
}
