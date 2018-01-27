using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Bloom.Common;
using Bloom.Data.Repositories;
using Bloom.LibraryModule.WindowModels;
using Bloom.LibraryModule.Windows;
using Bloom.PubSubEvents;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Bloom.Services;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Bloom.Player.LibraryModule.Services
{
    public class LibraryService : LibraryBaseService, ILibraryService
    {
        public LibraryService(IUnityContainer container, IEventAggregator eventAggregator, IRegionManager regionManager,
            ILibraryConnectionRepository libraryConnectionRepository, ILibraryRepository libraryRepository, IPersonRepository personRepository, IUserRepository userRepository)
            : base(container, eventAggregator, regionManager, libraryConnectionRepository, libraryRepository, personRepository, userRepository)
        {

            // Subscribe to events
            EventAggregator.GetEvent<ShowConnectedLibrariesModalEvent>().Subscribe(ShowConnectedLibrariesModal);
            EventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public PlayerState State { get; private set; }

        private void SetState(object nothing)
        {
            State = (PlayerState) RegionManager.Regions[Settings.MenuRegion].Context;
        }

        public void ShowConnectedLibrariesModal(object nothing)
        {
            ShowConnectedLibrariesModal();
        }

        public void ShowConnectedLibrariesModal()
        {
            var connectedLibrariesWindowModel = new ConnectedLibrariesWindowModel(RegionManager);
            connectedLibrariesWindowModel.LibraryConnections = new ObservableCollection<LibraryConnection>();
            var allConnections = ListLibraryConnections();
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
            var connectedLibrariesWindow = new ConnectedLibrariesWindow(connectedLibrariesWindowModel, EventAggregator, this)
            {
                Owner = Application.Current.MainWindow
            };
            connectedLibrariesWindow.ShowDialog();
        }
    }
}
