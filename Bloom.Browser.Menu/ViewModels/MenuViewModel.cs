using System.Windows.Input;
using Bloom.Browser.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.Menu.ViewModels
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
        public MenuViewModel(IRegionManager regionManager)
        {
            State = (State) regionManager.Regions["MenuRegion"].Context;
            SetSkinCommand = new DelegateCommand<string>(SetSkin, CanSetSkin);
        }

        public State State { get; private set; }

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
            
        }
    }
}
