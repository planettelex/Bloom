using System;
using System.Windows;
using System.Windows.Input;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Analytics.MenuModule.ViewModels
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
            State = (AnalyticsState) regionManager.Regions["MenuRegion"].Context;
            CheckConnections(null);

            _eventAggregator.GetEvent<ConnectionAddedEvent>().Subscribe(CheckConnections);
            _eventAggregator.GetEvent<ConnectionRemovedEvent>().Subscribe(CheckConnections);

            // File Menu
            ExitApplicationCommand = new DelegateCommand<object>(ExitApplication, CanExitApplication);
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
            SetToggleSidebarVisibilityOption(State.SidebarVisible);
            ToggleSidebarVisibilityCommand = new DelegateCommand<object>(ToggleSidebarVisibility, CanToggleSidebarVisibility);
            SetSkinCommand = new DelegateCommand<string>(SetSkin, CanSetSkin);
            // Help Menu
            OpenGettingStartedTabCommand = new DelegateCommand<object>(OpenGettingStartedTab, CanOpenGettingStartedTab);
        }
        private readonly ISkinningService _skinningService;
        private readonly IProcessService _processService;
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public AnalyticsState State { get; private set; }

        public bool HasConnections
        {
            get { return _hasConnections; }
            set { SetProperty(ref _hasConnections, value); }
        }
        private bool _hasConnections;

        public void CheckConnections(object unused)
        {
            HasConnections = State != null && State.Connections != null && State.Connections.Count > 0;
            if (!HasConnections)
            {
                SetToggleSidebarVisibilityOption(false);
                _eventAggregator.GetEvent<HideSidebarEvent>().Publish(null);
            }
        }

        public void CheckConnections(Guid unused)
        {
            CheckConnections(null);
        }

        #region File Menu

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
            _eventAggregator.GetEvent<DuplicateTabEvent>().Publish(State.SelectedTabId);
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

        public ICommand OpenHomeTabCommand { get; set; }

        private bool CanOpenHomeTab(object nothing)
        {
            return true;
        }

        private void OpenHomeTab(object nothing)
        {
            _eventAggregator.GetEvent<NewHomeTabEvent>().Publish(null);
        }

        public string ToggleSidebarVisibilityOption
        {
            get { return _toggleSidebarVisibilityOption; }
            set { SetProperty(ref _toggleSidebarVisibilityOption, value); }
        }
        private string _toggleSidebarVisibilityOption;

        private void SetToggleSidebarVisibilityOption(bool isVisible)
        {
            if (isVisible)
                ToggleSidebarVisibilityOption = "Hide Sidebar";
            else
                ToggleSidebarVisibilityOption = "Show Sidebar";
        }

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
    }
}
