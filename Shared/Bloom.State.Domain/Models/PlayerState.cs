using System.Windows;
using System.Data.Linq.Mapping;
using Bloom.Common;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// The state of the player application.
    /// </summary>
    [Table(Name = "player_state")]
    public class PlayerState : BindableBase, IApplicationState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerState"/> class.
        /// </summary>
        public PlayerState()
        {
            var process = new BloomProcess(ProcessType.Player);
            ProcessName = process.Name;
            SkinName = Properties.Settings.Default.SkinName;
            WindowState = Properties.Settings.Default.WindowState;
            RecentWidth = Properties.Settings.Default.SidebarWidth;
            UpcomingWidth = Properties.Settings.Default.SidebarWidth;
            Connections = new Connections();
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
        /// Gets or sets the width of the recent column.
        /// </summary>
        [Column(Name = "recent_width")]
        public int RecentWidth
        {
            get { return _recentWidth; }
            set { SetProperty(ref _recentWidth, value); }
        }
        private int _recentWidth;

        /// <summary>
        /// Gets or sets the width of the upcoming column.
        /// </summary>
        [Column(Name = "upcoming_width")]
        public int UpcomingWidth
        {
            get { return _upcomingWidth; }
            set { SetProperty(ref _upcomingWidth, value); }
        }
        private int _upcomingWidth;

        /// <summary>
        /// Resets the width of the recent column to the default value.
        /// </summary>
        public void ResetRecentWidth()
        {
            RecentWidth = Properties.Settings.Default.SidebarWidth;
        }

        /// <summary>
        /// Resets the width of the upcoming column to the default value.
        /// </summary>
        public void ResetUpcomingWidth()
        {
            UpcomingWidth = Properties.Settings.Default.SidebarWidth;
        }
    }
}
