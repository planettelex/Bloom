using System.Collections.ObjectModel;
using System.Windows.Input;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.LibraryModule.WindowModels
{
    public class ConnectedLibrariesWindowModel : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectedLibrariesWindowModel" /> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public ConnectedLibrariesWindowModel(IRegionManager regionManager)
        {
            State = (ApplicationState) regionManager.Regions[Common.Settings.MenuRegion].Context;
            Instructions = string.Format("Connect a library by specifing the path to its Bloom library file (*{0}).", Common.Settings.LibraryFileExtension);
        }
        
        /// <summary>
        /// Gets the state.
        /// </summary>
        public ApplicationState State { get; private set; }

        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        public string Instructions
        {
            get { return _instructions; }
            set { SetProperty(ref _instructions, value); }
        }
        private string _instructions;

        public ObservableCollection<LibraryConnection> LibraryConnections { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand DisconnectLibraryCommand { get; set; }

        public ICommand ConnectNewLibraryCommand { get; set; }

        public ICommand ConnectLibraryCommand { get; set; }

        public ICommand FindLibraryFileCommand { get; set; }

        public ICommand RemoveConnectionCommand { get; set; }
    }
}
