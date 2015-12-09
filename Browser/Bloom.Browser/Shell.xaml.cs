using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Bloom.Browser.State.Services;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
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
        /// <param name="stateService">The state service.</param>
        public Shell(ISkinningService skinningService, IEventAggregator eventAggregator, IBrowserStateService stateService)
        {
            InitializeComponent();
            _tabs = new Dictionary<Guid, RadPane>();
            _eventAggregator = eventAggregator;
            _stateService = stateService;
            var state = _stateService.InitializeState();
            DataContext = state;

            // Don't open in a minimized state.
            if (state.WindowState == WindowState.Minimized)
                state.WindowState = WindowState.Normal;

            WindowState = state.WindowState;
            TitleBar.SetButtonVisibilties();
            skinningService.SetSkin(state.SkinName);

            eventAggregator.GetEvent<AddTabEvent>().Subscribe(AddTab);
            eventAggregator.GetEvent<CloseOtherTabsEvent>().Subscribe(CloseOtherTabs);
            eventAggregator.GetEvent<CloseAllTabsEvent>().Subscribe(CloseAllTabs);
        }
        private readonly Dictionary<Guid, RadPane> _tabs;
        private readonly IBrowserStateService _stateService;
        private readonly IEventAggregator _eventAggregator;
        private BrowserState State { get { return (BrowserState) DataContext; } }

        #region Window Events

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.ContentRendered" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            RestoreTabs();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Activated" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            // TODO: Check state database for new messages.
            var shutUpResharper = "Stubbed Method";
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            State.WindowState = WindowState;
            _stateService.SaveState();
        }

        #endregion

        #region Docking Events

        private void AddTab(TabControl tabControl)
        {
            var tabHeader = new TabHeader(_eventAggregator)
            {
                TabId = tabControl.Id,
                Text = tabControl.Header,
                ViewMenuVisibility = tabControl.ShowViewMenu ? Visibility.Visible : Visibility.Collapsed
            };
            var newPane = new RadPane
            {
                Header = tabHeader,
                Content = tabControl.Content
            };

            _stateService.AddTab(tabControl.Tab);
            _tabs.Add(tabControl.Id, newPane);
            PaneGroup.Items.Add(newPane);
        }

        private void CloseOtherTabs(object nothing)
        {
            _stateService.RemoveAllTabsExcept(State.SelectedTabId);

            var selectedTab = GetSelectedTab();
            foreach (var tab in _tabs.Values)
            {
                if (selectedTab != null && !Equals(selectedTab, tab)) { }
                    tab.IsHidden = true;
            }
        }

        private void CloseAllTabs(object nothing)
        {
            _stateService.RemoveAllTabs();

            foreach (var tab in _tabs.Values)
                tab.IsHidden = true;
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
            _stateService.RemoveTab(State.SelectedTabId);

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

        private void OnSidebarSplitterDoubleClick(object sender, MouseButtonEventArgs e)
        {
            State.ResetSidebarWidth();
        }

        private void RestoreTabs()
        {
            if (State.Tabs == null || State.Tabs.Count == 0)
                _eventAggregator.GetEvent<NewHomeTabEvent>().Publish(null);
            else
            {
                foreach (var tab in State.Tabs)
                {
                    switch (tab.Type)
                    {
                        case TabType.Album:
                            _eventAggregator.GetEvent<RestoreAlbumTabEvent>().Publish(tab);
                            break;
                        case TabType.Artist:
                            _eventAggregator.GetEvent<RestoreArtistTabEvent>().Publish(tab);
                            break;
                        case TabType.Home:
                            _eventAggregator.GetEvent<RestoreHomeTabEvent>().Publish(tab);
                            break;
                        case TabType.Library:
                            _eventAggregator.GetEvent<RestoreLibraryTabEvent>().Publish(tab);
                            break;
                        case TabType.Person:
                            _eventAggregator.GetEvent<RestorePersonTabEvent>().Publish(tab);
                            break;
                        case TabType.Playlist:
                            _eventAggregator.GetEvent<RestorePlaylistTabEvent>().Publish(tab);
                            break;
                        case TabType.Song:
                            _eventAggregator.GetEvent<RestoreSongTabEvent>().Publish(tab);
                            break;
                    }
                }
            }
        }

        #endregion
    }
}
