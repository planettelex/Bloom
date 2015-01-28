using System.Windows;

namespace Bloom.State.Domain.Models
{
    public class PlayerState
    {
        public string ProcessName { get; set; }

        public string SkinName { get; set; }

        public WindowState WindowState { get; set; }

        public int PlayedWidth { get; set; }

        public int UpcomingWidth { get; set; }
    }
}
