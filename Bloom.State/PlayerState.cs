using System.Windows;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.State
{
    public class PlayerState : BindableBase
    {
        public PlayerState()
        {
            ProcessName = Properties.Settings.Default.Player_ProcessName;
            WindowState = Properties.Settings.Default.Player_WindowState;
            SkinName = Properties.Settings.Default.Player_SkinName;
            PlayedWidth = Properties.Settings.Default.Global_SidebarWidth;
            UpcomingWidth = Properties.Settings.Default.Global_SidebarWidth;
        }

        public string ProcessName { get; set; }

        public string SkinName { get; set; }

        public WindowState WindowState { get; set; }

        public int PlayedWidth
        {
            get { return _playedWidth; }
            set { SetProperty(ref _playedWidth, value); }
        }
        private int _playedWidth;

        public int UpcomingWidth
        {
            get { return _upcomingWidth; }
            set { SetProperty(ref _upcomingWidth, value); }
        }
        private int _upcomingWidth;
    }
}
