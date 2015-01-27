using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Analytics.Common;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Bloom.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace Bloom.Analytics
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
            _tabs = new Dictionary<Guid, RadPane>();

            skinningService.SetSkin(state.Skin);

            eventAggregator.GetEvent<AddTabEvent>().Subscribe(AddTab);
            eventAggregator.GetEvent<CloseOtherTabsEvent>().Subscribe(CloseOtherTabs);
            eventAggregator.GetEvent<CloseAllTabsEvent>().Subscribe(CloseAllTabs);
        }
        private readonly Dictionary<Guid, RadPane> _tabs;
        private State State { get { return (State) DataContext; } }

        #region Window Events

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Activated" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            // TODO: Check state database for new messages.
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            State.Save();
        }

        #endregion

        #region Docking Events

        private void AddTab(Tab tab)
        {
            var newPane = new RadPane
            {
                Header = tab.Header,
                Content = tab.Content
            };

            _tabs.Add(tab.Id, newPane);
            PaneGroup.Items.Add(newPane);
            State.SelectedTabId = tab.Id;
        }

        private void CloseOtherTabs(object nothing)
        {
            var selectedTab = GetSelectedTab();
            foreach (var tab in _tabs.Values)
            {
                if (selectedTab != null && !Equals(selectedTab, tab))
                    tab.IsHidden = true;
            }
        }

        private void CloseAllTabs(object nothing)
        {
            foreach (var tab in _tabs.Values)
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

        private void ActivePaneChanged(object sender, ActivePangeChangedEventArgs e)
        {
            foreach (var valuePair in _tabs.Where(valuePair => Equals(valuePair.Value, e.NewPane)))
            {
                State.SelectedTabId = valuePair.Key;
                break;
            }
        }

        private void OnClose(object sender, StateChangeEventArgs e)
        {
            var selectedTab = GetSelectedTab();
            if (selectedTab == null)
            {
                State.SelectedTabId = Guid.Empty;
                return;
            }

            foreach (var valuePair in _tabs.Where(valuePair => Equals(valuePair.Value, selectedTab)))
            {
                State.SelectedTabId = valuePair.Key;
            }
        }

        private RadPane GetSelectedTab()
        {
            RadPane selectedTab = null;
            foreach (RadPane tab in PaneGroup.Items)
            {
                if (tab.IsSelected)
                    selectedTab = tab;
            }
            return selectedTab;
        }

        #endregion
    }
}
