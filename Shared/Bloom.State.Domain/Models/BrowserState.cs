using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Windows;
using Bloom.Common;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// The state of the browser application.
    /// </summary>
    [Table(Name = "browser_state")]
    public class BrowserState : BindableBase, IApplicationState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserState"/> class.
        /// </summary>
        public BrowserState()
        {
            var process = new BloomProcess(ProcessType.Browser);
            ProcessName = process.Name;
            SkinName = Properties.Settings.Default.SkinName;
            WindowState = Properties.Settings.Default.WindowState;
            SidebarWidth = Properties.Settings.Default.SidebarWidth;
            Connections = new Connections();
            Tabs = new List<Tab>();
            SelectedTabId = Guid.Empty;
        }

        /// <summary>
        /// Gets or sets the name of the process.
        /// </summary>
        [Column(Name = "process_name", IsPrimaryKey = true)]
        public string ProcessName { get; set; }

        /// <summary>
        /// Gets or sets the data connections for the suite.
        /// </summary>
        public Connections Connections { get; set; }

        /// <summary>
        /// Gets or sets the name of the skin.
        /// </summary>
        [Column(Name = "skin_name")]
        public string SkinName { get; set; }

        /// <summary>
        /// Gets or sets the state of the window.
        /// </summary>
        [Column(Name = "window_state")]
        public WindowState WindowState { get; set; }

        /// <summary>
        /// Gets or sets the width of the sidebar column.
        /// </summary>
        [Column(Name = "sidebar_width")]
        public int SidebarWidth
        {
            get { return _sidebarWidth; }
            set { SetProperty(ref _sidebarWidth, value); }
        }
        private int _sidebarWidth;

        /// <summary>
        /// Gets or sets the selected tab identifier.
        /// </summary>
        [Column(Name = "selected_tab_id")]
        public Guid SelectedTabId { get; set; }

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

        /// <summary>
        /// Resets the width of the sidebar to the default value.
        /// </summary>
        public void ResetSidebarWidth()
        {
            SidebarWidth = Properties.Settings.Default.SidebarWidth;
        }
    }
}
