﻿using System.Windows.Input;
using Bloom.Analytics.Common;
using Bloom.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Analytics.Menu.ViewModels
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
        public MenuViewModel(IRegionManager regionManager, ISkinningService skinningService)
        {
            State = (State) regionManager.Regions["MenuRegion"].Context;
            SkinningService = skinningService;
            SetSkinCommand = new DelegateCommand<string>(SetSkin, CanSetSkin);
        }

        /// <summary>
        /// Gets the application state.
        /// </summary>
        public State State { get; private set; }

        /// <summary>
        /// Gets the skinning service.
        /// </summary>
        public ISkinningService SkinningService { get; private set; }

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
            if (State.Skin == skinName)
                return;

            State.Skin = skinName;
            SkinningService.SetSkin(skinName);
        }
    }
}