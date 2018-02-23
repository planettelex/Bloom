using System;
using System.Windows;
using System.Windows.Input;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Events;
using Prism.Regions;

namespace Bloom.Player.Modules.MenuModule.ViewModels
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

            _eventAggregator.GetEvent<ConnectionAddedEvent>().Subscribe(CheckConnections);
            _eventAggregator.GetEvent<ConnectionRemovedEvent>().Subscribe(CheckConnections);
            _eventAggregator.GetEvent<UserChangedEvent>().Subscribe(SetState);
            _eventAggregator.GetEvent<UserUpdatedEvent>().Subscribe(SetUser);

            // File Menu
            ManageConnectedLibrariesCommand = new DelegateCommand<object>(ManageConnectedLibraries, CanManageConnectedLibraries);
            ExitApplicationCommand = new DelegateCommand<object>(ExitApplication, CanExitApplication);
            // Browser Menu
            GoToBrowserCommand = new DelegateCommand<object>(GoToBrowser, CanGoToBrowser);
            // Analytics Menu
            GoToAnalyticsCommand = new DelegateCommand<object>(GoToAnalytics, CanGoToAnalytics);
            // View Menu
            SetSkinCommand = new DelegateCommand<string>(SetSkin, CanSetSkin);
            // User Menu
            UserProfileCommand = new DelegateCommand<object>(ShowUserProfile, CanShowUserProfile);
            ChangeUserCommand = new DelegateCommand<object>(ChangeUser, CanChangeUser);
        }
        private readonly ISkinningService _skinningService;
        private readonly IProcessService _processService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public PlayerState State { get; private set; }

        public void SetState(object nothing)
        {
            SetState();
        }

        public void SetState()
        {
            State = (PlayerState) _regionManager.Regions[Common.Settings.MenuRegion].Context;
            CheckConnections(null);
            SetUser(null);
        }

        #region Shared Properties

        public bool HasConnections
        {
            get { return _hasConnections; }
            set { SetProperty(ref _hasConnections, value); }
        }
        private bool _hasConnections;

        public void CheckConnections(object unused)
        {
            HasConnections = State?.Connections != null && State.Connections.Count > 0;
        }

        public void CheckConnections(Guid unused)
        {
            CheckConnections(null);
        }

        public bool HasUser
        {
            get { return _hasUser; }
            set { SetProperty(ref _hasUser, value); }
        }
        private bool _hasUser;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _userName;

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

        #region User Menu

        public ICommand ChangeUserCommand { get; set; }

        private bool CanChangeUser(object nothing)
        {
            return true;
        }

        private void ChangeUser(object nothing)
        {
            _eventAggregator.GetEvent<ShowChangeUserModalEvent>().Publish(null);
        }

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
