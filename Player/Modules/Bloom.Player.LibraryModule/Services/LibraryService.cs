using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Bloom.Common;
using Bloom.LibraryModule.WindowModels;
using Bloom.LibraryModule.Windows;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Player.LibraryModule.Services
{
    public class LibraryService : ILibraryService
    {
        public LibraryService(IEventAggregator eventAggregator, IRegionManager regionManager, ISharedLibraryService sharedLibraryService)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _sharedLibraryService = sharedLibraryService;

            // Subscribe to events
            _eventAggregator.GetEvent<ShowConnectedLibrariesModalEvent>().Subscribe(ShowConnectedLibrariesModal);
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly ISharedLibraryService _sharedLibraryService;
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public PlayerState State { get; private set; }

        private void SetState(object nothing)
        {
            State = (PlayerState) _regionManager.Regions[Settings.MenuRegion].Context;
        }

        public void ShowConnectedLibrariesModal(object nothing)
        {
            ShowConnectedLibrariesModal();
        }

        public void ShowConnectedLibrariesModal()
        {
            var connectedLibrariesWindowModel = new ConnectedLibrariesWindowModel(_regionManager);
            connectedLibrariesWindowModel.LibraryConnections = new ObservableCollection<LibraryConnection>();
            var allConnections = _sharedLibraryService.ListLibraryConnections();
            foreach (var connection in allConnections)
            {
                if (connection.IsConnected)
                {
                    var connectedLibrary = State.Connections.SingleOrDefault(c => c.LibraryId == connection.LibraryId);
                    if (connectedLibrary == null)
                        connection.IsConnected = false;
                    else
                        connection.DataSource = connectedLibrary.DataSource;
                }
                connection.SetButtonVisibilities();
            }
            connectedLibrariesWindowModel.LibraryConnections.AddRange(allConnections);
            var connectedLibrariesWindow = new ConnectedLibrariesWindow(connectedLibrariesWindowModel, _eventAggregator, _sharedLibraryService)
            {
                Owner = Application.Current.MainWindow
            };
            connectedLibrariesWindow.ShowDialog();
        }
    }
}
