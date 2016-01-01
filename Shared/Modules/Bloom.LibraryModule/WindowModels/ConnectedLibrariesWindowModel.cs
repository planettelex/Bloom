using System.Collections.ObjectModel;
using System.Windows.Input;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.LibraryModule.WindowModels
{
    public class ConnectedLibrariesWindowModel : BindableBase
    {
        public ConnectedLibrariesWindowModel(IRegionManager regionManager, ILibraryService libraryService)
        {
            _libraryService = libraryService;
            State = (ApplicationState) regionManager.Regions["DocumentRegion"].Context;
            Instructions = string.Format("Connect a library by specifing the path to its Bloom library file (*{0}).", Common.Settings.LibraryFileExtension);

            SetConnections();
        }
        private readonly ILibraryService _libraryService;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public ApplicationState State { get; private set; }

        public string Instructions
        {
            get { return _instructions; }
            set { SetProperty(ref _instructions, value); }
        }
        private string _instructions;

        public ObservableCollection<LibraryConnection> LibraryConnections { get; set; }

        public ICommand CloseCommand { get; set; }

        private void SetConnections()
        {
            LibraryConnections = new ObservableCollection<LibraryConnection>();
            LibraryConnections.AddRange(_libraryService.ListLibraryConnections());
        }
    }
}
