using System;
using System.IO;
using System.Windows;
using Bloom.Browser.PubSubEvents;
using Bloom.Common.ExtensionMethods;
using Bloom.Data;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Bloom.LibraryModule.WindowModels;
using Bloom.LibraryModule.Windows;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Bloom.LibraryModule.Services
{
    public class SharedLibraryService : ISharedLibraryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedLibraryService" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="libraryService">The library service.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="libraryRepository">The library repository.</param>
        /// <param name="personRepository">The person repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public SharedLibraryService(IUnityContainer container, IEventAggregator eventAggregator, IRegionManager regionManager, ILibraryService libraryService, 
            ILibraryConnectionRepository libraryConnectionRepository, ILibraryRepository libraryRepository, IPersonRepository personRepository, IUserRepository userRepository)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _libraryConnectionRepository = libraryConnectionRepository;
            _personRepository = personRepository;
            _libraryRepository = libraryRepository;
            _libraryService = libraryService;
            _userRepository = userRepository;

            // Subscribe to events
            _eventAggregator.GetEvent<CreateNewLibraryEvent>().Subscribe(CreateNewLibrary);
            _eventAggregator.GetEvent<ShowConnectedLibrariesModalEvent>().Subscribe(ShowConnectedLibrariesModal);

            State = (ApplicationState) regionManager.Regions["DocumentRegion"].Context;
        }
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly ILibraryService _libraryService;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public ApplicationState State { get; private set; }

        public void CreateNewLibrary(Library library)
        {
            if (library == null)
                throw new ArgumentNullException("library");

            var dataSource = new LibraryDataSource(_container);
            dataSource.Create(library.FilePath);
            var libraryConnection = LibraryConnection.Create(library);
            _libraryService.ConnectLibrary(libraryConnection, State.User, true, false);
            _libraryConnectionRepository.AddLibraryConnection(libraryConnection);
            _personRepository.AddPerson(libraryConnection.DataSource, library.Owner);
            _libraryRepository.AddLibrary(libraryConnection.DataSource, library);
            libraryConnection.SaveChanges();
            State.Connections.Insert(0, libraryConnection);

            _eventAggregator.GetEvent<SaveStateEvent>().Publish(null);
            _eventAggregator.GetEvent<ConnectionAddedEvent>().Publish(libraryConnection);
        }

        public LibraryConnection ConnectNewLibrary(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Library file does not exist", filePath);

            var dataSource = new LibraryDataSource(_container);
            dataSource.Connect(filePath);
            var library = _libraryRepository.GetLibrary(dataSource);
            if (library == null)
                return null;

            library.FileName = filePath.GetFileName();
            library.FolderPath = filePath.GetFilePath();
            var libraryConnection = LibraryConnection.Create(library);
            var existingConnection = _libraryConnectionRepository.GetLibraryConnection(library.Id);
            if (existingConnection != null)
                libraryConnection = existingConnection;
            
            var success = _libraryService.ConnectLibrary(libraryConnection, State.User, true, true);
            if (!success)
                return null;

            var existingUser = _userRepository.GetUser(library.OwnerId);
            if (existingUser == null)
                _userRepository.AddUser(User.Create(library.Owner));
            else
                _libraryService.SyncLibraryOwnerAndUser(libraryConnection, existingUser);

            _libraryConnectionRepository.AddLibraryConnection(libraryConnection);
            var alreadyConnected = State.Connections.Contains(libraryConnection);
            if (!alreadyConnected)
            {
                State.Connections.Insert(0, libraryConnection);
                _eventAggregator.GetEvent<SaveStateEvent>().Publish(null);
                _eventAggregator.GetEvent<ConnectionAddedEvent>().Publish(libraryConnection);
            }
            
            return libraryConnection;
        }

        public void ShowConnectedLibrariesModal(object nothing)
        {
            ShowConnectedLibrariesModal();
        }

        public void ShowConnectedLibrariesModal()
        {
            var connectedLibrariesWindowModel = new ConnectedLibrariesWindowModel(_regionManager, _libraryService);
            var connectedLibrariesWindow = new ConnectedLibrariesWindow(connectedLibrariesWindowModel, _eventAggregator, _libraryService, this)
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
