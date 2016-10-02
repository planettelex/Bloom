using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Bloom.Data.Repositories;
using Bloom.LibraryModule.WindowModels;
using Bloom.LibraryModule.Windows;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Bloom.LibraryModule.Services
{
    /// <summary>
    /// Service for shared library operations.
    /// </summary>
    /// <seealso cref="Bloom.Services.LibraryBaseService" />
    /// <seealso cref="Bloom.LibraryModule.Services.ISharedLibraryService" />
    public class SharedLibraryService : LibraryBaseService, ISharedLibraryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedLibraryService"/> class.
        /// </summary>
        /// <param name="container">The DI container.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="libraryRepository">The library repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public SharedLibraryService(IUnityContainer container, IEventAggregator eventAggregator, IRegionManager regionManager,
            ILibraryConnectionRepository libraryConnectionRepository, ILibraryRepository libraryRepository, IUserRepository userRepository)
            : base(container, eventAggregator, regionManager, libraryConnectionRepository, libraryRepository, userRepository)
        {
            // Subscribe to events
            EventAggregator.GetEvent<ShowConnectedLibrariesModalEvent>().Subscribe(ShowConnectedLibrariesModal);
        }

        /// <summary>
        /// Shows the connected libraries modal window.
        /// </summary>
        public void ShowConnectedLibrariesModal(object nothing)
        {
            ShowConnectedLibrariesModal();
        }

        /// <summary>
        /// Shows the connected libraries modal window.
        /// </summary>
        public void ShowConnectedLibrariesModal()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var connectedLibrariesWindowModel = new ConnectedLibrariesWindowModel(RegionManager, EventAggregator);
            connectedLibrariesWindowModel.LibraryConnections = new ObservableCollection<LibraryConnection>();
            var allConnections = ListLibraryConnections();
            foreach (var connection in allConnections)
            {
                if (connection.IsConnected)
                {
                    var connectedLibrary = ApplicationState.Connections.SingleOrDefault(c => c.LibraryId == connection.LibraryId);
                    if (connectedLibrary == null)
                        connection.IsConnected = false;
                    else
                        connection.DataSource = connectedLibrary.DataSource;
                }
                connection.SetButtonVisibilities();
            }
            connectedLibrariesWindowModel.LibraryConnections.AddRange(allConnections);
            var connectedLibrariesWindow = new ConnectedLibrariesWindow(connectedLibrariesWindowModel, this)
            {
                Owner = Application.Current.MainWindow
            };
            connectedLibrariesWindow.ShowDialog();
        }
    }
}
