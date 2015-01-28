using System.Windows;
using System.Windows.Input;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Player.Menu.ViewModels
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
        public MenuViewModel(IRegionManager regionManager, ISkinningService skinningService, IProcessService processService)
        {
            State = (PlayerState) regionManager.Regions["MenuRegion"].Context;
            _skinningService = skinningService;
            _processService = processService;

            // File Menu
            ExitApplicationCommand = new DelegateCommand<object>(ExitApplication, CanExitApplication);
            // Browser Menu
            GoToBrowserCommand = new DelegateCommand<object>(GoToBrowser, CanGoToBrowser);
            // Analytics Menu
            GoToAnalyticsCommand = new DelegateCommand<object>(GoToAnalytics, CanGoToAnalytics);
            // View Menu
            SetSkinCommand = new DelegateCommand<string>(SetSkin, CanSetSkin);
        }
        private readonly ISkinningService _skinningService;
        private readonly IProcessService _processService;

        /// <summary>
        /// Gets the player application state.
        /// </summary>
        public PlayerState State { get; private set; }

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
    }
}
