using System.Windows;
using System.Windows.Input;
using Bloom.Browser.PubSubEvents;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.MenuModule.ViewModels
{
    /// <summary>
    /// View model for MenuView.xaml.
    /// </summary>
    public class MenuViewModel
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
            State = (BrowserState) regionManager.Regions["MenuRegion"].Context;
            _skinningService = skinningService;
            _processService = processService;
            _eventAggregator = eventAggregator;

            // File Menu
            CreateNewLibraryCommand = new DelegateCommand<object>(CreateNewLibrary, CanCreateNewLibrary);
            ExitApplicationCommand = new DelegateCommand<object>(ExitApplication, CanExitApplication);
            // Browser Menu
            DuplicateTabCommand = new DelegateCommand<object>(DuplicateTab, CanDuplicateTab);
            CloseOtherTabsCommand = new DelegateCommand<object>(CloseOtherTabs, CanCloseOtherTabs);
            CloseAllTabsCommand = new DelegateCommand<object>(CloseAllTabs, CanCloseAllTabs);
            // Player Menu
            GoToPlayerCommand = new DelegateCommand<object>(GoToPlayer, CanGoToPlayer);
            // Analytics Menu
            GoToAnalyticsCommand = new DelegateCommand<object>(GoToAnalytics, CanGoToAnalytics);
            // View Menu
            SetSkinCommand = new DelegateCommand<string>(SetSkin, CanSetSkin);
        }
        private readonly ISkinningService _skinningService;
        private readonly IProcessService _processService;
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public BrowserState State { get; private set; }

        #region File Menu

        /// <summary>
        /// Gets or sets the create new library command.
        /// </summary>
        public ICommand CreateNewLibraryCommand { get; set; }

        private bool CanCreateNewLibrary(object nothing)
        {
            return true;
        }

        private void CreateNewLibrary(object nothing)
        {
            _eventAggregator.GetEvent<ShowCreateNewLibraryModalEvent>().Publish(null);
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

        #region Browser Menu

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

        #region Analytics Menu

        /// <summary>
        /// Gets or sets the go to analytics command.
        /// </summary>
        public ICommand GoToAnalyticsCommand { get; set; }

        private bool CanGoToAnalytics(object nothing)
        {
            return true;
        }

        private void GoToAnalytics(object nothing)
        {
            _processService.GoToAnalyticsProcess();
        }

        #endregion

        #region View Menu

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
    }
}
