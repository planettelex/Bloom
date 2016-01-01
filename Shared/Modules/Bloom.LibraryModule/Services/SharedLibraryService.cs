using System.Windows;
using Bloom.Domain.Models;
using Bloom.LibraryModule.WindowModels;
using Bloom.LibraryModule.Windows;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.LibraryModule.Services
{
    public class SharedLibraryService : ISharedLibraryService
    {
        public SharedLibraryService(IEventAggregator eventAggregator, IRegionManager regionManager, ILibraryService libraryService)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _libraryService = libraryService;

            // Subscribe to events
            _eventAggregator.GetEvent<ShowConnectedLibrariesModalEvent>().Subscribe(ShowConnectedLibrariesModal);

            State = (ApplicationState) regionManager.Regions["DocumentRegion"].Context;
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly ILibraryService _libraryService;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public ApplicationState State { get; private set; }

        public void ShowConnectedLibrariesModal(object nothing)
        {
            ShowConnectedLibrariesModal();
        }

        public void ShowConnectedLibrariesModal()
        {
            var connectedLibrariesWindowModel = new ConnectedLibrariesWindowModel(_regionManager, _libraryService);
            var connectedLibrariesWindow = new ConnectedLibrariesWindow(connectedLibrariesWindowModel, _eventAggregator)
            {
                Owner = Application.Current.MainWindow
            };
            connectedLibrariesWindow.ShowDialog();
        }

        public void ShowLibraryPropertiesModal(Library library)
        {
            
        }
    }
}
