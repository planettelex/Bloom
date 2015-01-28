using System.Windows;
using System.Data.Linq.Mapping;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// The state of the player application.
    /// </summary>
    [Table(Name = "player_state")]
    public class PlayerState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerState"/> class.
        /// </summary>
        public PlayerState()
        {
            ProcessName = "Bloom.Player";
            SkinName = Properties.Settings.Default.SkinName;
            WindowState = Properties.Settings.Default.WindowState;
            PlayedWidth = Properties.Settings.Default.SidebarWidth;
            UpcomingWidth = Properties.Settings.Default.SidebarWidth;
        }

        /// <summary>
        /// Gets or sets the name of the process.
        /// </summary>
        [Column(Name = "process_name", IsPrimaryKey = true)]
        public string ProcessName { get; set; }

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
        /// Gets or sets the width of the played column.
        /// </summary>
        [Column(Name = "played_width")]
        public int PlayedWidth { get; set; }

        /// <summary>
        /// Gets or sets the width of the upcoming column.
        /// </summary>
        [Column(Name = "upcoming_width")]
        public int UpcomingWidth { get; set; }
    }
}
