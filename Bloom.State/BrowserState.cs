using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Bloom.Controls;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.State
{
    public class BrowserState : BindableBase
    {
        public BrowserState()
        {
            ProcessName = Properties.Settings.Default.Browser_ProcessName;
            WindowState = Properties.Settings.Default.Browser_WindowState;
            SkinName = Properties.Settings.Default.Browser_SkinName;
            SidebarWidth = Properties.Settings.Default.Global_SidebarWidth;

            Tabs = new List<Tab>();
            SelectedTabId = Guid.Empty;
        }

        public string ProcessName { get; private set; }

        public string SkinName { get; set; }

        public WindowState WindowState { get; set; }

        public Guid SelectedTabId { get; set; }

        public List<Tab> Tabs 
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
                HasTabs = _tabs.Any();
            }
        }
        private List<Tab> _tabs; 

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
