using System.Windows.Input;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.UserModule.WindowModels
{
    public class ChangeUserWindowModel : BindableBase
    {
        public ChangeUserWindowModel(IRegionManager regionManager)
        {
            State = (ApplicationState) regionManager.Regions[Common.Settings.MenuRegion].Context;
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public ApplicationState State { get; private set; }

        public ICommand CloseCommand { get; set; }
    }
}
