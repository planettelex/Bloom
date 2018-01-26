using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Windows;
using Bloom.Common;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// Represents the state of the browser application.
    /// </summary>
    [Table(Name = "browser_state")]
    public class BrowserState : TabbedApplicationState
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
            Connections = new List<LibraryConnection>();
            Tabs = new List<Tab>();
            SelectedTabId = Guid.Empty;
        }

        /// <summary>
        /// Gets or sets the name of the process.
        /// </summary>
        [Column(Name = "process_name", IsPrimaryKey = true)]
        public string ProcessName { get; set; }

        /// <summary>
        /// Gets or sets the user's person identifier.
        /// </summary>
        [Column(Name = "person_id", IsPrimaryKey = true)]
        public Guid UserId { get; set; }

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
        /// Gets or sets the selected tab identifier.
        /// </summary>
        [Column(Name = "selected_tab_id")]
        public Guid? SelectedTabId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sidebar is visible.
        /// </summary>
        [Column(Name = "sidebar_visible")]
        public bool SidebarVisible { get; set; }

        /// <summary>
        /// Gets or sets the width of the sidebar.
        /// </summary>
        [Column(Name = "sidebar_width")]
        public int SidebarWidth
        {
            get { return _sidebarWidth; }
            set { SetProperty(ref _sidebarWidth, value); }
        }
        private int _sidebarWidth;

        /// <summary>
        /// Resets the width of the sidebar to the default value.
        /// </summary>
        public void ResetSidebarWidth()
        {
            SidebarWidth = Properties.Settings.Default.SidebarWidth;
        }

        /// <summary>
        /// Sets the user and their login time to now.
        /// </summary>
        /// <param name="user">A user.</param>
        public override void SetUser(User user)
        {
            base.SetUser(user);
            UserId = user?.PersonId ?? Guid.Empty;
        }
    }
}
