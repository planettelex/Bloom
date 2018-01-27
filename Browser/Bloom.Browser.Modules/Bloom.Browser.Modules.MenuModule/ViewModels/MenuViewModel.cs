using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Bloom.Browser.PubSubEvents;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.Modules.MenuModule.ViewModels
{
    /// <summary>
    /// View model for MenuView.xaml.
    /// </summary>
    public class MenuViewModel : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewModel" /> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="skinningService">The skinning service.</param>
        /// <param name="processService">The process service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public MenuViewModel(IRegionManager regionManager, ISkinningService skinningService, IProcessService processService, IEventAggregator eventAggregator)
        {
            _skinningService = skinningService;
            _processService = processService;
            _regionManager = regionManager;
            EventAggregator = eventAggregator;

            EventAggregator.GetEvent<ConnectionAddedEvent>().Subscribe(SetState);
            EventAggregator.GetEvent<ConnectionRemovedEvent>().Subscribe(SetState);
            EventAggregator.GetEvent<UserChangedEvent>().Subscribe(SetState);
            EventAggregator.GetEvent<UserUpdatedEvent>().Subscribe(SetUser);
            EventAggregator.GetEvent<SidebarToggledEvent>().Subscribe(SetToggleSidebarVisibilityOption);
            EventAggregator.GetEvent<SelectedTabChangedEvent>().Subscribe(SetLibraryContext);
            EventAggregator.GetEvent<TabAddedEvent>().Subscribe(SetHasTabs);
            EventAggregator.GetEvent<TabClosedEvent>().Subscribe(SetHasTabs);
            EventAggregator.GetEvent<TabsClosedEvent>().Subscribe(SetHasTabs);
            
            // File Menu
            CreateNewLibraryCommand = new DelegateCommand<object>(CreateNewLibrary, CanCreateNewLibrary);
            ManageConnectedLibrariesCommand = new DelegateCommand<object>(ManageConnectedLibraries, CanManageConnectedLibraries);
            AddMusicCommand = new DelegateCommand<object>(AddMusic, CanAddMusic);
            ExitApplicationCommand = new DelegateCommand<object>(ExitApplication, CanExitApplication);
            // Edit Menu
            EditLibraryPropertiesCommand = new DelegateCommand<object>(EditLibraryProperties, CanEditLibraryProperties);
            // Browser Menu
            DuplicateTabCommand = new DelegateCommand<object>(DuplicateTab, CanDuplicateTab);
            CloseOtherTabsCommand = new DelegateCommand<object>(CloseOtherTabs, CanCloseOtherTabs);
            CloseAllTabsCommand = new DelegateCommand<object>(CloseAllTabs, CanCloseAllTabs);
            // Player Menu
            GoToPlayerCommand = new DelegateCommand<object>(GoToPlayer, CanGoToPlayer);
            // Analytics Menu
            GoToAnalyticsCommand = new DelegateCommand<object>(GoToAnalytics, CanGoToAnalytics);
            // View Menu
            OpenHomeTabCommand = new DelegateCommand<object>(OpenHomeTab, CanOpenHomeTab);
            ToggleSidebarVisibilityCommand = new DelegateCommand<object>(ToggleSidebarVisibility, CanToggleSidebarVisibility);
            SetSkinCommand = new DelegateCommand<string>(SetSkin, CanSetSkin);
            // Help Menu
            OpenGettingStartedTabCommand = new DelegateCommand<object>(OpenGettingStartedTab, CanOpenGettingStartedTab);
            // User Menu
            UserProfileCommand = new DelegateCommand<object>(ShowUserProfile, CanShowUserProfile);
            ChangeUserCommand = new DelegateCommand<object>(ChangeUser, CanChangeUser);
        }
        private readonly ISkinningService _skinningService;
        private readonly IProcessService _processService;
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        public IEventAggregator EventAggregator { get; }

        /// <summary>
        /// Gets the browser state.
        /// </summary>
        public BrowserState State { get; private set; }

        /// <summary>
        /// Sets the state.
        /// </summary>
        private void SetState(object nothing)
        {
            SetState();
        }

        /// <summary>
        /// Sets the state.
        /// </summary>
        /// <param name="libraryId">A library identifier.</param>
        private void SetState(Guid libraryId)
        {
            SetState();
        }

        /// <summary>
        /// Sets the state.
        /// </summary>
        public void SetState()
        {
            State = (BrowserState) _regionManager.Regions[Bloom.Common.Settings.MenuRegion].Context;
            CheckConnections();
            SetUser();
            SetHasTabs();
            SetLibraryContext(State.SelectedTabId);
            SetToggleSidebarVisibilityOption(State.SidebarVisible);
        }

        /// <summary>
        /// Gets or sets a value indicating whether there are library connections.
        /// </summary>
        public bool HasConnections
        {
            get { return _hasConnections; }
            set { SetProperty(ref _hasConnections, value); }
        }
        private bool _hasConnections;

        /// <summary>
        /// Checks the connections.
        /// </summary>
        private void CheckConnections()
        {
            HasConnections = State != null && State.HasConnections();
            if (!HasConnections)
            {
                SetToggleSidebarVisibilityOption(false);
                EventAggregator.GetEvent<HideSidebarEvent>().Publish(null);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the change user menu option visibility.
        /// </summary>
        public Visibility ChangeUserVisibility
        {
            get { return _changeUserVisibility; }
            set { SetProperty(ref _changeUserVisibility, value); }
        }
        private Visibility _changeUserVisibility;

        /// <summary>
        /// Gets or sets a value indicating whether the new user menu option visibility.
        /// </summary>
        public Visibility NewUserVisibility
        {
            get { return _newUserVisibility; }
            set { SetProperty(ref _newUserVisibility, value); }
        }
        private Visibility _newUserVisibility;

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _userName;

        /// <summary>
        /// Sets the user.
        /// </summary>
        private void SetUser(object nothing)
        {
            SetUser();
        }

        /// <summary>
        /// Sets the user.
        /// </summary>
        private void SetUser()
        {
            if (State?.User?.Name == null || State.UserId == User.Anonymous.PersonId)
            {
                ChangeUserVisibility = Visibility.Collapsed;
                NewUserVisibility = Visibility.Visible;
            }
            else
            {
                UserName = State.User.Name;
                ChangeUserVisibility = Visibility.Visible;
                NewUserVisibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the active tab has a library associated with it.
        /// </summary>
        public bool HasLibraryContext
        {
            get { return _hasLibraryContext; }
            set { SetProperty(ref _hasLibraryContext, value); }
        }
        private bool _hasLibraryContext;

        /// <summary>
        /// Sets the library context.
        /// </summary>
        /// <param name="tabId">A tab identifier.</param>
        private void SetLibraryContext(Guid? tabId)
        {
            if (tabId != null)
                SetLibraryContext(tabId.Value);
        }

        /// <summary>
        /// Sets the library context.
        /// </summary>
        /// <param name="tabId">A tab identifier.</param>
        private void SetLibraryContext(Guid tabId)
        {
            if (State.Tabs == null)
                HasLibraryContext = false;
            else
            {
                var selectedTab = State.Tabs.SingleOrDefault(tab => tab.Id == tabId);
                HasLibraryContext = selectedTab != null && selectedTab.HasLibraryContext();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether there are any tabs.
        /// </summary>
        public bool HasTabs
        {
            get { return _hasTabs; }
            set { SetProperty(ref _hasTabs, value); }
        }
        private bool _hasTabs;

        /// <summary>
        /// Gets or sets a value indicating whether there are multiple tabs.
        /// </summary>
        public bool HasMultipleTabs
        {
            get { return _hasMulipleTabs; }
            set { SetProperty(ref _hasMulipleTabs, value); }
        }
        private bool _hasMulipleTabs;

        /// <summary>
        /// Sets the tab indicator properties.
        /// </summary>
        /// <param name="tabId">A tab identifier that has been added or removed.</param>
        private void SetHasTabs(Guid tabId)
        {
            SetHasTabs();
        }

        /// <summary>
        /// Sets the tab indicator properties.
        /// </summary>
        private void SetHasTabs(object nothing)
        {
            SetHasTabs();
        }

        /// <summary>
        /// Sets the tab indicator properties.
        /// </summary>
        private void SetHasTabs()
        {
            if (State == null || !State.HasTabs())
            {
                HasTabs = false;
                HasMultipleTabs = false;
            }
            else
            {
                HasTabs = true;
                HasMultipleTabs = State.Tabs.Count > 1;
            }
        }

        #region File Menu

        /// <summary>
        /// Gets or sets the create new library command.
        /// </summary>
        public ICommand CreateNewLibraryCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the create new libary command.
        /// </summary>
        private bool CanCreateNewLibrary(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The new create new libary command.
        /// </summary>
        private void CreateNewLibrary(object nothing)
        {
            EventAggregator.GetEvent<ShowCreateNewLibraryModalEvent>().Publish(null);
        }

        /// <summary>
        /// Gets or sets the manage connected libraries command.
        /// </summary>
        public ICommand ManageConnectedLibrariesCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the manage connected libraries command.
        /// </summary>
        private bool CanManageConnectedLibraries(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The manage connected libraries command.
        /// </summary>
        private void ManageConnectedLibraries(object nothing)
        {
            EventAggregator.GetEvent<ShowConnectedLibrariesModalEvent>().Publish(null);
        }

        /// <summary>
        /// Gets or sets the add music command.
        /// </summary>
        public ICommand AddMusicCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the add music command.
        /// </summary>
        private bool CanAddMusic(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The add music command.
        /// </summary>
        private void AddMusic(object nothing)
        {
            EventAggregator.GetEvent<ShowAddMusicModalEvent>().Publish(null);
        }

        /// <summary>
        /// Gets or sets the exit application command.
        /// </summary>
        public ICommand ExitApplicationCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the exit application command.
        /// </summary>
        private bool CanExitApplication(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The exit application command.
        /// </summary>
        private void ExitApplication(object nothing)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region Edit Menu

        /// <summary>
        /// Gets or sets the edit library properties command.
        /// </summary>
        public ICommand EditLibraryPropertiesCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the edit library properties command.
        /// </summary>
        private bool CanEditLibraryProperties(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The edit library properties command.
        /// </summary>
        private void EditLibraryProperties(object nothing)
        {
            EventAggregator.GetEvent<ShowLibraryPropertiesModalEvent>().Publish(null);
        }

        #endregion

        #region Browser Menu

        /// <summary>
        /// Gets or sets the duplicate tab command.
        /// </summary>
        public ICommand DuplicateTabCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the duplicate tab command.
        /// </summary>
        private bool CanDuplicateTab(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The duplicate tab command.
        /// </summary>
        private void DuplicateTab(object nothing)
        {
            if (State.SelectedTabId != null)
                EventAggregator.GetEvent<DuplicateTabEvent>().Publish(State.SelectedTabId.Value);
        }

        /// <summary>
        /// Gets or sets the close other tabs command.
        /// </summary>
        public ICommand CloseOtherTabsCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the close other tabs command.
        /// </summary>
        private bool CanCloseOtherTabs(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The close other tabs command.
        /// </summary>
        private void CloseOtherTabs(object nothing)
        {
            EventAggregator.GetEvent<CloseOtherTabsEvent>().Publish(null);
        }

        /// <summary>
        /// Gets or sets the close all tabs command.
        /// </summary>
        public ICommand CloseAllTabsCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the close all tabs command.
        /// </summary>
        private bool CanCloseAllTabs(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The close all tabs event.
        /// </summary>
        private void CloseAllTabs(object nothing)
        {
            EventAggregator.GetEvent<CloseAllTabsEvent>().Publish(null);
        }

        #endregion

        #region Player Menu

        /// <summary>
        /// Gets or sets the go to player command.
        /// </summary>
        public ICommand GoToPlayerCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the go to player command.
        /// </summary>
        private bool CanGoToPlayer(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The go to player command.
        /// </summary>
        private void GoToPlayer(object nothing)
        {
            _processService.GoToPlayerProcess();
        }

        #endregion

        #region Analytics Menu

        /// <summary>
        /// Gets or sets the go to analytics command.
        /// </summary>
        public ICommand GoToAnalyticsCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the go to analytics command.
        /// </summary>
        private bool CanGoToAnalytics(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The go to analytics command.
        /// </summary>
        private void GoToAnalytics(object nothing)
        {
            _processService.GoToAnalyticsProcess();
        }

        #endregion

        #region View Menu

        /// <summary>
        /// Gets or sets the open home tab command.
        /// </summary>
        public ICommand OpenHomeTabCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the open home tab command.
        /// </summary>
        private bool CanOpenHomeTab(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The open home tab command.
        /// </summary>
        private void OpenHomeTab(object nothing)
        {
            EventAggregator.GetEvent<NewHomeTabEvent>().Publish(null);
        }

        /// <summary>
        /// Gets or sets the toggle sidebar visibility option.
        /// </summary>
        public string ToggleSidebarVisibilityOption
        {
            get { return _toggleSidebarVisibilityOption; }
            set { SetProperty(ref _toggleSidebarVisibilityOption, value); }
        }
        private string _toggleSidebarVisibilityOption;

        /// <summary>
        /// Sets the toggle sidebar visibility option.
        /// </summary>
        /// <param name="isVisible">When set to <c>true</c> the sidebar is visible.</param>
        private void SetToggleSidebarVisibilityOption(bool isVisible)
        {
            ToggleSidebarVisibilityOption = isVisible ? "Hide Sidebar" : "Show Sidebar";
        }

        /// <summary>
        /// Gets or sets the toggle sidebar visibility command.
        /// </summary>
        public ICommand ToggleSidebarVisibilityCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the toggle sidebar visibility command.
        /// </summary>
        private bool CanToggleSidebarVisibility(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The toggle sidebar visibiity command.
        /// </summary>
        private void ToggleSidebarVisibility(object nothing)
        {
            SetToggleSidebarVisibilityOption(!State.SidebarVisible);
            if (State.SidebarVisible)
                EventAggregator.GetEvent<HideSidebarEvent>().Publish(null);
            else
                EventAggregator.GetEvent<ShowSidebarEvent>().Publish(null);
        }

        /// <summary>
        /// Gets or sets the set skin command.
        /// </summary>
        public ICommand SetSkinCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the set skin command.
        /// </summary>
        private bool CanSetSkin(string skinName)
        {
            return true;
        }

        /// <summary>
        /// The set skin command.
        /// </summary>
        /// <param name="skinName">Name of the skin.</param>
        private void SetSkin(string skinName)
        {
            if (State.SkinName == skinName)
                return;

            State.SkinName = skinName;
            _skinningService.SetSkin(skinName);
        }

        #endregion

        #region Help Menu

        /// <summary>
        /// Gets or sets the open getting started tab command.
        /// </summary>
        public ICommand OpenGettingStartedTabCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the open getting started tab event.
        /// </summary>
        private bool CanOpenGettingStartedTab(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The open getting started tab event.
        /// </summary>
        private void OpenGettingStartedTab(object nothing)
        {
            EventAggregator.GetEvent<NewGettingStartedTabEvent>().Publish(null);
        }

        #endregion

        #region User Menu

        /// <summary>
        /// Gets or sets the change user command.
        /// </summary>
        public ICommand ChangeUserCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the change user command.
        /// </summary>
        private bool CanChangeUser(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The change user command.
        /// </summary>
        private void ChangeUser(object nothing)
        {
            EventAggregator.GetEvent<ShowChangeUserModalEvent>().Publish(null);
        }

        /// <summary>
        /// Gets or sets the user profile command.
        /// </summary>
        public ICommand UserProfileCommand { get; set; }

        /// <summary>
        /// Determines whether this instance can use the show user profile command.
        /// </summary>
        private bool CanShowUserProfile(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The show user profile command.
        /// </summary>
        private void ShowUserProfile(object nothing)
        {
            if (State.User == null)
                EventAggregator.GetEvent<ShowChangeUserModalEvent>().Publish(null);
            else
                EventAggregator.GetEvent<ShowUserProfileModalEvent>().Publish(null);
        }

        #endregion
    }
}
