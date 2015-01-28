using System.Collections.Generic;
using System.Windows;
using Bloom.Controls;

namespace Bloom.State.Domain.Models
{
    public class AnalyticsState
    {
        public string ProcessName { get; set; }

        public string SkinName { get; set; }

        public WindowState WindowState { get; set; }

        public List<Tab> Tabs { get; set; }

        public int SidebarWidth { get; set; }
    }
}
