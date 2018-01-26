using System.Collections.ObjectModel;
using System.Windows.Input;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.LibraryModule.WindowModels
{
    /// <summary>
    /// Window model for ConnectedLibrariesWindow.xaml
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Prism.Mvvm.BindableBase" />
    public class ConnectedLibrariesWindowModel : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectedLibrariesWindowModel" /> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public ConnectedLibrariesWindowModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            State = (ApplicationState) regionManager.Regions[Common.Settings.MenuRegion].Context;
            EventAggregator = eventAggregator;
            Instructions = $"Connect a library by specifing the path to its Bloom library file (*{Common.Settings.LibraryFileExtension}).";
        }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        public IEventAggregator EventAggregator { get; private set; }
        
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

        /// <summary>
        /// Gets or sets the library connections.
        /// </summary>
        public ObservableCollection<LibraryConnection> LibraryConnections { get; set; }

        /// <summary>
        /// Gets or sets the close command.
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// Gets or sets the disconnect library command.
        /// </summary>
        public ICommand DisconnectLibraryCommand { get; set; }

        /// <summary>
        /// Gets or sets the connect new library command.
        /// </summary>
        public ICommand ConnectNewLibraryCommand { get; set; }

        /// <summary>
        /// Gets or sets the connect library command.
        /// </summary>
        public ICommand ConnectLibraryCommand { get; set; }

        /// <summary>
        /// Gets or sets the find library file command.
        /// </summary>
        public ICommand FindLibraryFileCommand { get; set; }

        /// <summary>
        /// Gets or sets the remove connection command.
        /// </summary>
        public ICommand RemoveConnectionCommand { get; set; }
    }
}
