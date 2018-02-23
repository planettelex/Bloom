using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Events;
using Prism.Regions;

namespace Bloom.Analytics.Modules.MenuModule.ViewModels
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
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            _eventAggregator.GetEvent<ConnectionAddedEvent>().Subscribe(SetState);
            _eventAggregator.GetEvent<ConnectionRemovedEvent>().Subscribe(SetState);
            _eventAggregator.GetEvent<UserChangedEvent>().Subscribe(SetState);
            _eventAggregator.GetEvent<UserUpdatedEvent>().Subscribe(SetUser);
            _eventAggregator.GetEvent<SidebarToggledEvent>().Subscribe(SetToggleSidebarVisibilityOption);
            _eventAggregator.GetEvent<SelectedTabChangedEvent>().Subscribe(SetLibraryContext);

            // File Menu
            ManageConnectedLibrariesCommand = new DelegateCommand<object>(ManageConnectedLibraries, CanManageConnectedLibraries);
            ExitApplicationCommand = new DelegateCommand<object>(ExitApplication, CanExitApplication);
            // Edit Menu
            EditLibraryPropertiesCommand = new DelegateCommand<object>(EditLibraryProperties, CanEditLibraryProperties);
            // Analytics Menu
            DuplicateTabCommand = new DelegateCommand<object>(DuplicateTab, CanDuplicateTab);
            CloseOtherTabsCommand = new DelegateCommand<object>(CloseOtherTabs, CanCloseOtherTabs);
            CloseAllTabsCommand = new DelegateCommand<object>(CloseAllTabs, CanCloseAllTabs);
            // Browser Menu
            GoToBrowserCommand = new DelegateCommand<object>(GoToBrowser, CanGoToBrowser);
            // Player Menu
            GoToPlayerCommand = new DelegateCommand<object>(GoToPlayer, CanGoToPlayer);
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
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Gets the analytics state.
        /// </summary>
        public AnalyticsState State { get; private set; }

        /// <summary>
        /// Sets the state.
        /// </summary>
        public void SetState(object nothing)
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
            State = (AnalyticsState) _regionManager.Regions[Common.Settings.MenuRegion].Context;
            CheckConnections();
            SetUser(null);
            SetLibraryContext(State.SelectedTabId);
            SetToggleSidebarVisibilityOption(State.SidebarVisible);
        }

        #region Shared Properties

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
        public void CheckConnections()
        {
            HasConnections = State?.Connections != null && State.Connections.Count > 0;
            if (!HasConnections)
            {
                SetToggleSidebarVisibilityOption(false);
                _eventAggregator.GetEvent<HideSidebarEvent>().Publish(null);
            }
        }

        /// <summary>
        /// Gets or sets whether the application has a user.
        /// </summary>
        public bool HasUser
        {
            get { return _hasUser; }
            set { SetProperty(ref _hasUser, value); }
        }
        private bool _hasUser;

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
        public void SetUser(object nothing)
        {
            if (State?.User?.Name == null)
            {
                UserName = "Login";
                HasUser = false;
            }
            else
            {
                UserName = State.User.Name;
                HasUser = true;
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
        /// The identifier of the current library context.
        /// </summary>
        public Guid LibraryContext { get; set; }

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
            var selectedTab = State.Tabs.SingleOrDefault(tab => tab.Id == tabId);
            if (selectedTab?.LibraryId == null)
            {
                LibraryContext = Guid.Empty;
                HasLibraryContext = false;
            }
            else
            {
                LibraryContext = selectedTab.LibraryId.Value;
                HasLibraryContext = true;
            }
        }

        #endregion

        #region File Menu

        /// <summary>
        /// Gets or sets the manage connected libraries command.
        /// </summary>
        public ICommand ManageConnectedLibrariesCommand { get; set; }

        private bool CanManageConnectedLibraries(object nothing)
        {
            return true;
        }

        private void ManageConnectedLibraries(object nothing)
        {
            _eventAggregator.GetEvent<ShowConnectedLibrariesModalEvent>().Publish(null);
        }

        /// <summary>
        /// Gets or sets the exit application command.
        /// </summary>
        public ICommand ExitApplicationCommand { get; set; }

        private bool CanExitApplication(object nothing)
        {
            return true;
        }

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

        private bool CanEditLibraryProperties(object nothing)
        {
            return true;
        }

        private void EditLibraryProperties(object nothing)
        {
            _eventAggregator.GetEvent<ShowLibraryPropertiesModalEvent>().Publish(LibraryContext);
        }

        #endregion

        #region Analytics Menu

        /// <summary>
        /// Gets or sets the duplicate tab command.
        /// </summary>
        public ICommand DuplicateTabCommand { get; set; }

        private bool CanDuplicateTab(object nothing)
        {
            return true;
        }

        private void DuplicateTab(object nothing)
        {
            if (State.SelectedTabId != null)
                _eventAggregator.GetEvent<DuplicateTabEvent>().Publish(State.SelectedTabId.Value);
        }

        /// <summary>
        /// Gets or sets the close other tabs command.
        /// </summary>
        public ICommand CloseOtherTabsCommand { get; set; }

        private bool CanCloseOtherTabs(object nothing)
        {
            return true;
        }

        private void CloseOtherTabs(object nothing)
        {
            _eventAggregator.GetEvent<CloseOtherTabsEvent>().Publish(null);
        }

        /// <summary>
        /// Gets or sets the close all tabs command.
        /// </summary>
        public ICommand CloseAllTabsCommand { get; set; }

        private bool CanCloseAllTabs(object nothing)
        {
            return true;
        }

        private void CloseAllTabs(object nothing)
        {
            _eventAggregator.GetEvent<CloseAllTabsEvent>().Publish(null);
        }

        #endregion

        #region Browser Menu

        /// <summary>
        /// Gets or sets the go to browser command.
        /// </summary>
        public ICommand GoToBrowserCommand { get; set; }

        private bool CanGoToBrowser(object nothing)
        {
            return true;
        }

        private void GoToBrowser(object nothing)
        {
            _processService.GoToBrowserProcess();
        }

        #endregion

        #region Player Menu

        /// <summary>
        /// Gets or sets the go to player command.
        /// </summary>
        public ICommand GoToPlayerCommand { get; set; }

        private bool CanGoToPlayer(object nothing)
        {
            return true;
        }

        private void GoToPlayer(object nothing)
        {
            _processService.GoToPlayerProcess();
        }

        #endregion

        #region View Menu

        /// <summary>
        /// Gets or sets the open home tab command.
        /// </summary>
        public ICommand OpenHomeTabCommand { get; set; }

        private bool CanOpenHomeTab(object nothing)
        {
            return true;
        }

        private void OpenHomeTab(object nothing)
        {
            _eventAggregator.GetEvent<NewHomeTabEvent>().Publish(null);
        }

        /// <summary>
        /// Gets or sets the sidebar visibility option.
        /// </summary>
        public string ToggleSidebarVisibilityOption
        {
            get { return _toggleSidebarVisibilityOption; }
            set { SetProperty(ref _toggleSidebarVisibilityOption, value); }
        }
        private string _toggleSidebarVisibilityOption;

        private void SetToggleSidebarVisibilityOption(bool isVisible)
        {
            ToggleSidebarVisibilityOption = isVisible ? "Hide Sidebar" : "Show Sidebar";
        }

        /// <summary>
        /// Gets or sets the toggle sidebar visibility command.
        /// </summary>
        public ICommand ToggleSidebarVisibilityCommand { get; set; }

        private bool CanToggleSidebarVisibility(object nothing)
        {
            return true;
        }

        private void ToggleSidebarVisibility(object nothing)
        {
            SetToggleSidebarVisibilityOption(!State.SidebarVisible);
            if (State.SidebarVisible)
                _eventAggregator.GetEvent<HideSidebarEvent>().Publish(null);
            else
                _eventAggregator.GetEvent<ShowSidebarEvent>().Publish(null);
        }

        /// <summary>
        /// Gets or sets the set skin command.
        /// </summary>
        public ICommand SetSkinCommand { get; set; }

        private bool CanSetSkin(string skinName)
        {
            return true;
        }

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

        private bool CanOpenGettingStartedTab(object nothing)
        {
            return true;
        }

        private void OpenGettingStartedTab(object nothing)
        {
            _eventAggregator.GetEvent<NewGettingStartedTabEvent>().Publish(null);
        }

        #endregion

        #region User Menu

        /// <summary>
        /// Gets or sets the change user tab command.
        /// </summary>
        public ICommand ChangeUserCommand { get; set; }

        private bool CanChangeUser(object nothing)
        {
            return true;
        }

        private void ChangeUser(object nothing)
        {
            _eventAggregator.GetEvent<ShowChangeUserModalEvent>().Publish(null);
        }

        /// <summary>
        /// Gets or sets the view user profile command.
        /// </summary>
        public ICommand UserProfileCommand { get; set; }

        private bool CanShowUserProfile(object nothing)
        {
            return true;
        }

        private void ShowUserProfile(object nothing)
        {
            if (State.User == null)
                _eventAggregator.GetEvent<ShowChangeUserModalEvent>().Publish(null);
            else
                _eventAggregator.GetEvent<ShowUserProfileModalEvent>().Publish(null);
        }

        #endregion
    }
}
