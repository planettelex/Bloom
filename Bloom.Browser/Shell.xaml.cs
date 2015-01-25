using System.Collections.Generic;
using System.Windows.Controls;
using Bloom.Browser.Common;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Bloom.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace Bloom.Browser
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shell" /> class.
        /// </summary>
        /// <param name="skinningService">The skinning service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public Shell(ISkinningService skinningService, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            var state = new State();
            DataContext = state;
            _tabs = new List<RadPane>();

            skinningService.SetSkin(state.Skin);

            eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateTab);
            eventAggregator.GetEvent<AddTabEvent>().Subscribe(AddTab);
            eventAggregator.GetEvent<CloseOtherTabsEvent>().Subscribe(CloseOtherTabs);
            eventAggregator.GetEvent<CloseAllTabsEvent>().Subscribe(CloseAllTabs);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            var state = (State) DataContext;
            state.Save();
        }

        private void AddTab(Tab tab)
        {
            var newPane = new RadPane
            {
                Header = tab.Header,
                Content = tab.Content
            };

            _tabs.Add(newPane);
            PaneGroup.Items.Add(newPane);
        }

        private void DuplicateTab(object nothing)
        {
            var activeTab = GetActiveTab();
            if (activeTab == null)
                return;

            var duplicateTab = new Tab
            {
                Header = (string) activeTab.Header,
                Content = (UserControl) activeTab.Content.XamlClone()
            };
            AddTab(duplicateTab);
        }

        private void CloseOtherTabs(object nothing)
        {
            var activeTab = GetActiveTab();
            foreach (var tab in _tabs)
            {
                if (activeTab != null && !Equals(activeTab, tab))
                    tab.IsHidden = true;
            }
        }

        private RadPane GetActiveTab()
        {
            RadPane activeTab = null;
            foreach (RadPane tab in PaneGroup.Items)
            {
                if (tab.IsActive)
                    activeTab = tab;
            }
            return activeTab;
        }

        private void CloseAllTabs(object nothing)
        {
            foreach (var tab in _tabs)
            {
                tab.IsHidden = true;
            }
        }

        private void DockCompassPreview(object sender, PreviewShowCompassEventArgs e)
        {
            if (e.TargetGroup != null && e.TargetGroup.Name == "Sidebar")
            {
                e.Compass.IsLeftIndicatorVisible = false;
                e.Compass.IsTopIndicatorVisible = false;
                e.Compass.IsRightIndicatorVisible = false;
                e.Compass.IsBottomIndicatorVisible = false;
                e.Compass.IsCenterIndicatorVisible = false;
            }
            else
            {
                e.Compass.IsLeftIndicatorVisible = false;
                e.Compass.IsTopIndicatorVisible = false;
                e.Compass.IsRightIndicatorVisible = false;
                e.Compass.IsBottomIndicatorVisible = false;
                e.Compass.IsCenterIndicatorVisible = true;
            }
        }

        private readonly List<RadPane> _tabs;
    }
}
