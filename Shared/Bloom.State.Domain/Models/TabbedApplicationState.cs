using System;
using System.Collections.Generic;
using System.Linq;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// Application state for tabbed applications.
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
            if (Tabs == null || Tabs.Count == 0)
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
            if (Tabs == null || Tabs.Count == 0)
                return;

            for (var i = 0; i < Tabs.Count; i++)
                Tabs[i].Order = i + 1;
        }

        public override void SetUser(User user)
        {
            base.SetUser(user);
            if (Tabs == null || Tabs.Count <= 0)
                return;

            foreach (var tab in Tabs)
                tab.UserId = user.PersonId;
        }

        public bool HasLibraryTabs(Guid libraryId)
        {
            return Tabs.Any(tab => tab.LibraryId == libraryId);
        }
    }
}
