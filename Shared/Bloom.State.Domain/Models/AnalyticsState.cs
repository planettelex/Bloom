using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Windows;
using Bloom.Common;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// The state of the analytics application.
    /// </summary>
    [Table(Name = "analytics_state")]
    public class AnalyticsState : TabbedApplicationState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsState"/> class.
        /// </summary>
        public AnalyticsState()
        {
            var process = new BloomProcess(ProcessType.Analytics);
            ProcessName = process.Name;
            SkinName = Properties.Settings.Default.SkinName;
            WindowState = Properties.Settings.Default.WindowState;
            SidebarWidth = Properties.Settings.Default.SidebarWidth;
            Connections = new List<LibraryConnection>();
            Tabs = new List<Tab>();
            SelectedTabId = Guid.Empty;
        }

        [Column(Name = "process_name", IsPrimaryKey = true)]
        public string ProcessName { get; set; }

        [Column(Name = "person_id", IsPrimaryKey = true)]
        public Guid UserId { get; set; }

        [Column(Name = "skin_name")]
        public string SkinName { get; set; }

        [Column(Name = "window_state")]
        public WindowState WindowState { get; set; }

        [Column(Name = "selected_tab_id")]
        public Guid SelectedTabId { get; set; }

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

        public override void SetUser(User user)
        {
            base.SetUser(user);
            UserId = user.PersonId;
        }
    }
}
