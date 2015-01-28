using System;
using System.Collections.Generic;
using System.Windows;
using Bloom.Controls;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.State
{
    public class AnalyticsState : BindableBase
    {
        public AnalyticsState()
        {
            ProcessName = Properties.Settings.Default.Analytics_ProcessName;
            WindowState = Properties.Settings.Default.Analytics_WindowState;
            SkinName = Properties.Settings.Default.Analytics_SkinName;
            SidebarWidth = Properties.Settings.Default.Global_SidebarWidth;

            Tabs = new List<Tab>();
            SelectedTabId = Guid.Empty;
        }

        public string ProcessName { get; set; }

        public string SkinName { get; set; }

        public WindowState WindowState { get; set; }

        public List<Tab> Tabs { get; set; }

        public Guid SelectedTabId { get; set; }

        public bool HasTabs
        {
            get { return _hasTabs; }
            set { SetProperty(ref _hasTabs, value); }
        }
        private bool _hasTabs;

        public int SidebarWidth
        {
            get { return _sidebarWidth; }
            set { SetProperty(ref _sidebarWidth, value); }
        }
        private int _sidebarWidth;
    }
}
