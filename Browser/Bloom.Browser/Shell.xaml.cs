using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Bloom.Browser.Controls;
using Bloom.Browser.PubSubEvents;
using Bloom.Browser.State.Services;
using Bloom.Common;
using Bloom.Controls;
using Bloom.Modules.LibraryModule.Services;
using Bloom.Modules.UserModule.Services;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Prism.Events;
using Telerik.Windows;
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
        /// <param name="sharedUserService">The shared user service.</param>
        /// <param name="sharedLibraryService">The shared library service.</param>
        /// <param name="stateService">The state service.</param>
        public Shell(ISkinningService skinningService, IEventAggregator eventAggregator, ISharedUserService sharedUserService, ISharedLibraryService sharedLibraryService, IBrowserStateService stateService)
        {
            InitializeComponent();
            _loading = true;
            _tabs = new Dictionary<Guid, RadPane>();
            _eventAggregator = eventAggregator;
            _sharedLibraryService = sharedLibraryService;
            _sharedUserService = sharedUserService;
            _skinningService = skinningService;
            _stateService = stateService;
            _stateService.ConnectDataSource();
            var user = _sharedUserService.InitializeUser();
            var state = _stateService.InitializeState(user);
            DataContext = state;

            // Don't open in a minimized state.
            if (state.WindowState == WindowState.Minimized)
                state.WindowState = WindowState.Normal;

            WindowState = state.WindowState;
            TitleBar.SetButtonVisibilties();
            SidebarPane.IsHidden = !state.SidebarVisible;
            _skinningService.SetSkin(state.SkinName);

            _eventAggregator.GetEvent<AddTabEvent>().Subscribe(AddTab);
            _eventAggregator.GetEvent<CloseOtherTabsEvent>().Subscribe(CloseOtherTabs);
            _eventAggregator.GetEvent<CloseAllTabsEvent>().Subscribe(CloseAllTabs);
            _eventAggregator.GetEvent<CloseTabEvent>().Subscribe(CloseTab);
            _eventAggregator.GetEvent<HideSidebarEvent>().Subscribe(HideSidebar);
            _eventAggregator.GetEvent<ShowSidebarEvent>().Subscribe(ShowSidebar);
            _eventAggregator.GetEvent<ConnectionAddedEvent>().Subscribe(ShowSidebar);
            _eventAggregator.GetEvent<ConnectionRemovedEvent>().Subscribe(CheckConnections);
            _eventAggregator.GetEvent<ChangeUserEvent>().Subscribe(ChangeUser);
            _eventAggregator.GetEvent<UserChangedEvent>().Subscribe(SetupFromState);
        }
        private readonly Dictionary<Guid, RadPane> _tabs;
        private readonly IBrowserStateService _stateService;
        private readonly ISharedLibraryService _sharedLibraryService;
        private readonly ISharedUserService _sharedUserService;
        private readonly ISkinningService _skinningService;
        private readonly IEventAggregator _eventAggregator;
        private bool _loading;

        /// <summary>
        /// Gets the browser state.
        /// </summary>
        public BrowserState State => (BrowserState) DataContext;

        #region User Events

        /// <summary>
        /// Changes the user.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        private void ChangeUser(User newUser)
        {
            if (newUser == null)
                return;

            if (State.UserId == newUser.PersonId)
                return;

            _sharedUserService.AddUser(newUser);

            // Persist open tabs only when switching from anonymous user.
            var tabCount = State.Tabs?.Count ?? 0;
            var openTabs = new Tab[tabCount];
            if (State.UserId == User.Anonymous.PersonId && State.Tabs != null)
            {
                State.Tabs.CopyTo(openTabs);
                _eventAggregator.GetEvent<CloseAllTabsEvent>().Publish(null);
            }
            else
                _eventAggregator.GetEvent<SaveStateEvent>().Publish(State);

            DataContext = _stateService.InitializeState(newUser);

            foreach (var tab in openTabs)
            {
                if (tab != null)
                    _stateService.AddTab(Tab.Create(ProcessType.Browser, State.User, Buid.Empty, State.GetNextTabOrder(), tab.Type, tab.Header));
            }

            _eventAggregator.GetEvent<SaveStateEvent>().Publish(State);
            _eventAggregator.GetEvent<UserChangedEvent>().Publish(null);
        }

        /// <summary>
        /// Sets up application settings and tabs from the active state object.
        /// </summary>
        private void SetupFromState(object nothing)
        {
            _skinningService.SetSkin(State.SkinName);

            // Don't automatically minimize the application.
            if (State.WindowState == WindowState.Minimized)
                State.WindowState = WindowState.Normal;

            WindowState = State.WindowState;
            SidebarPane.IsHidden = !State.SidebarVisible;

            foreach (var tab in _tabs.Values)
                tab.IsHidden = true;

            _tabs.Clear();

            var selectedTabId = State.SelectedTabId;
            _stateService.RestoreTabs();
            if (selectedTabId != null)
                SetSelectedTab(selectedTabId.Value);
        }

        #endregion

        #region Window Events

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.ContentRendered" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            _loading = false;
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Publish(null);

            var selectedTabId = State.SelectedTabId;
            _stateService.RestoreTabs();
            if (selectedTabId != null) 
                SetSelectedTab(selectedTabId.Value);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Activated" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (_loading)
                return;

            var lastProcessToAccessState = _stateService.LastProcessToAccessState();
            var processChanged = lastProcessToAccessState != ProcessType.Browser && lastProcessToAccessState != ProcessType.None;
            if (!processChanged)
                return;

            _stateService.ChangeStateProcess(ProcessType.Browser);
            _sharedLibraryService.CheckLibraryConnections();
            _sharedUserService.CheckUser();
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

        /// <summary>
        /// Adds a tab to the docking control.
        /// </summary>
        /// <param name="tabControl">A tab control.</param>
        private void AddTab(TabControl tabControl)
        {
            if (_tabs.ContainsKey(tabControl.TabId))
                _tabs[tabControl.TabId].IsHidden = false;
            else
            {
                var titleTemplate = (DataTemplate) FindResource("TitleTemplate");
                var newPane = new RadPane
                {
                    Content = tabControl.Content,
                    HeaderTemplate = titleTemplate,
                    TitleTemplate = titleTemplate,
                    Title = tabControl,
                    Tag = tabControl.TabId
                };

                tabControl.Tab.UserId = State.UserId;
                _stateService.AddTab(tabControl.Tab);
                if (!_loading || State.SelectedTabId == Guid.Empty)
                    State.SelectedTabId = tabControl.TabId;

                _tabs.Add(tabControl.TabId, newPane);
                PaneGroup.Items.Add(newPane);
            }
            _eventAggregator.GetEvent<SaveStateEvent>().Publish(State);
            _eventAggregator.GetEvent<TabAddedEvent>().Publish(tabControl.TabId);
        }

        /// <summary>
        /// Closes a tab.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        private void CloseTab(Guid tabId)
        {
            _stateService.RemoveTab(tabId);

            var tab = _tabs[tabId];
            if (tab != null)
                tab.IsHidden = true;

            _eventAggregator.GetEvent<SaveStateEvent>().Publish(State);
            _eventAggregator.GetEvent<TabClosedEvent>().Publish(tabId);
        }

        /// <summary>
        /// Closes all tabs except the currently active one.
        /// </summary>
        private void CloseOtherTabs(object nothing)
        {
            if (State.SelectedTabId == null)
                CloseAllTabs();
            else
            {
                _stateService.RemoveAllTabsExcept(State.SelectedTabId.Value);
                var selectedTab = GetSelectedTab();
                foreach (var tab in _tabs.Values)
                {
                    if (selectedTab != null && !Equals(selectedTab, tab))
                        tab.IsHidden = true;
                }
                _eventAggregator.GetEvent<SaveStateEvent>().Publish(State);
                _eventAggregator.GetEvent<TabsClosedEvent>().Publish(null);
            }
        }

        /// <summary>
        /// Closes all tabs.
        /// </summary>
        private void CloseAllTabs(object nothing = null)
        {
            _stateService.RemoveAllTabs();

            foreach (var tab in _tabs.Values)
                tab.IsHidden = true;

            _eventAggregator.GetEvent<SaveStateEvent>().Publish(State);
            _eventAggregator.GetEvent<TabsClosedEvent>().Publish(null);
        }

        /// <summary>
        /// Occurs when the active pane (tab) has changed.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">The <see cref="ActivePangeChangedEventArgs"/> instance containing the event data.</param>
        private void ActivePaneChanged(object sender, ActivePangeChangedEventArgs e)
        {
            if (_loading)
                return;

            foreach (var valuePair in _tabs.Where(valuePair => Equals(valuePair.Value, e.NewPane)))
            {
                State.SelectedTabId = valuePair.Key;
                _eventAggregator.GetEvent<SelectedTabChangedEvent>().Publish(State.SelectedTabId.Value);
                break;
            }

            _eventAggregator.GetEvent<SaveStateEvent>().Publish(State);
        }

        /// <summary>
        /// Occurs when a tab is closed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="StateChangeEventArgs"/> instance containing the event data.</param>
        private void OnClose(object sender, StateChangeEventArgs e)
        {
            if (_loading)
                return;

            var closingTab = e.Panes.SingleOrDefault();

            foreach (var valuePair in _tabs)
            {
                var match = Equals(valuePair.Value, closingTab);
                if (match)
                {
                    var tabId = valuePair.Key;
                    _stateService.RemoveTab(tabId);
                    _eventAggregator.GetEvent<SaveStateEvent>().Publish(State);
                    _eventAggregator.GetEvent<TabClosedEvent>().Publish(tabId);
                    break;
                }
            }
        }

        /// <summary>
        /// Occurs when a pane (tab) state has changed.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">The <see cref="RadRoutedEventArgs"/> instance containing the event data.</param>
        private void PaneStateChanged(object sender, RadRoutedEventArgs e)
        {
            if (_loading)
                return;

            var order = 1;
            foreach (RadPane pane in PaneGroup.Items)
            {
                UpdateTabOrder(pane, order);
                order++;
            }
            foreach (var pane in Dock.Panes.Where(pane => !PaneGroup.Items.Contains(pane)))
            {
                UpdateTabOrder(pane, order);
                order++;
            }
        }

        /// <summary>
        /// Occurs when a view menu item has been clicked on.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">The <see cref="RadRoutedEventArgs"/> instance containing the event data.</param>
        private void ViewMenuClick(object sender, RadRoutedEventArgs e)
        {
            var menuItem = (RadMenuItem) sender;
            var tabId = (Guid) menuItem.Tag;
            var viewType = (ViewType) Enum.Parse(typeof(ViewType), menuItem.Name);
            _eventAggregator.GetEvent<ChangeTabViewEvent>().Publish(new Tuple<Guid, ViewType>(tabId, viewType));
        }

        /// <summary>
        /// Updates the tab order.
        /// </summary>
        /// <param name="pane">The pane (tab).</param>
        /// <param name="order">The tab order.</param>
        private void UpdateTabOrder(RadPane pane, int order)
        {
            var tabId = _tabs.SingleOrDefault(valuePair => Equals(valuePair.Value, pane)).Key;
            var tab = State.Tabs.SingleOrDefault(t => t.Id == tabId);
            if (tab == null)
                return;

            tab.Order = order;
        }

        /// <summary>
        /// Gets the selected tab.
        /// </summary>
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

        /// <summary>
        /// Gets the tab.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        private RadPane GetTab(Guid tabId)
        {
            RadPane selectedTab = null;
            foreach (RadPane tab in PaneGroup.Items)
            {
                if (((Guid) tab.Tag) == tabId)
                    selectedTab = tab;
            }
            return selectedTab;
        }

        /// <summary>
        /// Sets the selected tab.
        /// </summary>
        private void SetSelectedTab(Guid tabId)
        {
            var selectedTab = GetTab(tabId);
            Dock.ActivePane = selectedTab;
        }

        /// <summary>
        /// Occurs when the docking compass is shown.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">The <see cref="PreviewShowCompassEventArgs"/> instance containing the event data.</param>
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

        #endregion

        #region Sidebar Events

        /// <summary>
        /// Occurs when the divider between the dock and sidebar has been double clicked.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void SidebarSplitterDoubleClick(object sender, MouseButtonEventArgs e)
        {
            State.ResetSidebarWidth();
            _eventAggregator.GetEvent<SaveStateEvent>().Publish(State);
        }

        /// <summary>
        /// Shows the sidebar.
        /// </summary>
        private void ShowSidebar(object nothing)
        {
            State.SidebarVisible = true;
            SidebarPane.IsHidden = false;
            _eventAggregator.GetEvent<SaveStateEvent>().Publish(State);
            _eventAggregator.GetEvent<SidebarToggledEvent>().Publish(true);
        }

        /// <summary>
        /// Hides the sidebar.
        /// </summary>
        private void HideSidebar(object nothing)
        {
            State.SidebarVisible = false;
            SidebarPane.IsHidden = true;
            _eventAggregator.GetEvent<SaveStateEvent>().Publish(State);
            _eventAggregator.GetEvent<SidebarToggledEvent>().Publish(false);
        }

        /// <summary>
        /// Checks whether there are library connections, and hides the sidebar if there are none.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        public void CheckConnections(Guid libraryId)
        {
            if (State == null || !State.HasConnections())
                HideSidebar(null);
        }

        #endregion
    }
}
