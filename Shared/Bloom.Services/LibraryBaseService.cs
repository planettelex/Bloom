using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bloom.Data;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Bloom.Services
{
    /// <summary>
    /// Base class for library services.
    /// </summary>
    public class LibraryBaseService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryBaseService" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="libraryRepository">The library repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public LibraryBaseService(IUnityContainer container, IEventAggregator eventAggregator, IRegionManager regionManager,
            ILibraryConnectionRepository libraryConnectionRepository, ILibraryRepository libraryRepository, IUserRepository userRepository)
        {
            _container = container;
            _libraryConnectionRepository = libraryConnectionRepository;
            _libraryRepository = libraryRepository;
            _userRepository = userRepository;
            EventAggregator = eventAggregator;
            RegionManager = regionManager;

            // Subscribe to events
            EventAggregator.GetEvent<CreateNewLibraryEvent>().Subscribe(CreateNewLibrary);
            EventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
        }
        private readonly IUnityContainer _container;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;

        /// <summary>
        /// Gets the application state.
        /// </summary>
        public ApplicationState ApplicationState { get; private set; }

        /// <summary>
        /// Gets or sets the event aggregator.
        /// </summary>
        protected IEventAggregator EventAggregator { get; set; }

        /// <summary>
        /// Gets or sets the region manager.
        /// </summary>
        protected IRegionManager RegionManager { get; set; }

        /// <summary>
        /// Sets the application state in the menu region context.
        /// </summary>
        /// <param name="nothing">Unused object.</param>
        private void SetState(object nothing = null)
        {
            ApplicationState = (ApplicationState)RegionManager.Regions[Common.Settings.MenuRegion].Context;
        }

        /// <summary>
        /// Creates a new library.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <exception cref="System.ArgumentNullException">library</exception>
        private void CreateNewLibrary(Library library)
        {
            if (library == null)
                throw new ArgumentNullException("library");

            if (!Directory.Exists(library.FolderPath))
                Directory.CreateDirectory(library.FolderPath);

            var peopleFolder = library.FolderPath + "\\" + Properties.Settings.Default.PeopleFolder;
            if (!Directory.Exists(peopleFolder))
                Directory.CreateDirectory(peopleFolder);

            var artistsFolder = library.FolderPath + "\\" + Properties.Settings.Default.ArtistsFolder;
            if (!Directory.Exists(artistsFolder))
                Directory.CreateDirectory(artistsFolder);

            var mixedArtistFolder = artistsFolder + "\\" + Properties.Settings.Default.MixedArtistFolder;
            if (!Directory.Exists(mixedArtistFolder))
                Directory.CreateDirectory(mixedArtistFolder);

            var playlistsFolder = library.FolderPath + "\\" + Properties.Settings.Default.PlaylistsFolder;
            if (!Directory.Exists(playlistsFolder))
                Directory.CreateDirectory(playlistsFolder);

            var dataSource = new LibraryDataSource(_container);
            dataSource.Create(library.FilePath);
            var libraryConnection = LibraryConnection.Create(library);
            ConnectLibrary(libraryConnection, ApplicationState.User, true, false);
            _libraryConnectionRepository.AddLibraryConnection(libraryConnection);
            _libraryRepository.AddLibrary(libraryConnection.DataSource, library);
            libraryConnection.SaveChanges();
            ApplicationState.Connections.Insert(0, libraryConnection);

            EventAggregator.GetEvent<SaveStateEvent>().Publish(null);
            EventAggregator.GetEvent<ConnectionAddedEvent>().Publish(libraryConnection);
        }

        /// <summary>
        /// Makes a new connection to an existing library file.
        /// </summary>
        /// <param name="filePath">The library file path.</param>
        /// <exception cref="System.ArgumentNullException">filePath</exception>
        /// <exception cref="System.IO.FileNotFoundException">Library file does not exist</exception>
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

            library.FileName = Path.GetFileName(filePath);
            library.FolderPath = Path.GetDirectoryName(filePath);
            var libraryConnection = LibraryConnection.Create(library);
            var existingConnection = _libraryConnectionRepository.GetLibraryConnection(library.Id);
            if (existingConnection != null)
                libraryConnection = existingConnection;
            
            var success = ConnectLibrary(libraryConnection, ApplicationState.User, true, true);
            if (!success)
                return null;

            var existingUser = _userRepository.GetUser(library.OwnerId);
            if (existingUser == null)
                _userRepository.AddUser(User.Create(library.Owner));
            else
                SyncLibraryOwnerAndUser(libraryConnection, existingUser);

            _libraryConnectionRepository.AddLibraryConnection(libraryConnection);
            var alreadyConnected = ApplicationState.Connections.Contains(libraryConnection);
            if (!alreadyConnected)
            {
                ApplicationState.Connections.Insert(0, libraryConnection);
                EventAggregator.GetEvent<SaveStateEvent>().Publish(null);
                EventAggregator.GetEvent<ConnectionAddedEvent>().Publish(libraryConnection);
            }
            
            return libraryConnection;
        }

        /// <summary>
        /// Connects to the given library connection.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        /// <param name="user">The connecting user.</param>
        /// <param name="timestamp">If set to <c>true</c> set the last connected library connection property.</param>
        /// <param name="setLibrary">If set to <c>true</c> set the library property of the library connection from the library data source.</param>
        /// <exception cref="System.NullReferenceException">
        /// Library connection file path cannot be null.
        /// or
        /// User cannot be null.
        /// </exception>
        public bool ConnectLibrary(LibraryConnection libraryConnection, User user, bool timestamp, bool setLibrary)
        {
            if (libraryConnection == null)
                return false;

            if (libraryConnection.IsConnected && libraryConnection.DataSource != null && libraryConnection.DataSource.IsConnected())
                return true;

            if (string.IsNullOrEmpty(libraryConnection.FilePath))
                throw new NullReferenceException("Library connection file path cannot be null.");

            if (!File.Exists(libraryConnection.FilePath))
                libraryConnection.IsConnected = false;
            else
            {
                if (libraryConnection.DataSource == null)
                    libraryConnection.DataSource = new LibraryDataSource(_container);

                libraryConnection.DataSource.Connect(libraryConnection.FilePath);

                if (setLibrary)
                    libraryConnection.Library = _libraryRepository.GetLibrary(libraryConnection.DataSource);

                var userCreated = false;
                if (user == null && libraryConnection.Library != null)
                {
                    user = User.Create(libraryConnection.Library.Owner);
                    ApplicationState.SetUser(user);
                    userCreated = true;
                }

                if (user == null)
                    throw new NullReferenceException("User cannot be null.");

                if (userCreated)
                    EventAggregator.GetEvent<UserChangedEvent>().Publish(null);

                if (timestamp)
                {
                    libraryConnection.LastConnected = DateTime.Now;
                    if (libraryConnection.Library != null && libraryConnection.Library.OwnerId == user.PersonId)
                        libraryConnection.Library.OwnerLastConnected = libraryConnection.LastConnected;
                }

                libraryConnection.IsConnected = true;
                libraryConnection.LastConnectionBy = user.PersonId;
            }
            return libraryConnection.IsConnected;
        }

        /// <summary>
        /// Connects to the given libary connections.
        /// </summary>
        /// <param name="libraryConnections">The library connections.</param>
        /// <param name="user">The connecting user.</param>
        /// <param name="timestamp">If set to <c>true</c> set the last connected library connection property.</param>
        /// <param name="setLibrary">If set to <c>true</c> set the library property of the library connection from the library data source.</param>
        public void ConnectLibraries(List<LibraryConnection> libraryConnections, User user, bool timestamp, bool setLibrary)
        {
            if (libraryConnections == null || libraryConnections.Count == 0)
                return;

            var unsuccessfulConnections = new List<LibraryConnection>();

            foreach (var libraryConnection in libraryConnections)
            {
                var success = ConnectLibrary(libraryConnection, user, timestamp, setLibrary);
                if (success)
                    libraryConnection.Library = _libraryRepository.GetLibrary(libraryConnection.DataSource);
                else
                    unsuccessfulConnections.Add(libraryConnection);
            }

            foreach (var unsuccessfulConnection in unsuccessfulConnections)
                libraryConnections.Remove(unsuccessfulConnection);
        }

        /// <summary>
        /// Lists the library connections.
        /// </summary>
        public List<LibraryConnection> ListLibraryConnections()
        {
            var allConnections = _libraryConnectionRepository.ListLibraryConnections() ?? new List<LibraryConnection>();
            return allConnections.OrderBy(connection => connection.LibraryName).ToList();
        }

        /// <summary>
        /// Removes the given library connection.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        public void RemoveLibraryConnection(LibraryConnection libraryConnection)
        {
            _libraryConnectionRepository.DeleteLibraryConnection(libraryConnection);
        }

        /// <summary>
        /// Checks the library connections for additions and removals.
        /// </summary>
        public void CheckLibraryConnections()
        {
            if (ApplicationState == null)
                return;

            if (ApplicationState.Connections == null)
                ApplicationState.Connections = new List<LibraryConnection>();

            var connections = _libraryConnectionRepository.ListLibraryConnections();
            
            foreach (var connection in connections)
            {
                if (connection.IsConnected && !ApplicationState.Connections.Contains(connection))
                {
                    ConnectLibrary(connection, ApplicationState.User, false, true);
                    ApplicationState.Connections.Add(connection);
                    EventAggregator.GetEvent<ConnectionAddedEvent>().Publish(connection);
                }
                if (!connection.IsConnected && ApplicationState.Connections.Contains(connection))
                {
                    connection.Disconnect();
                    ApplicationState.RemoveConnection(connection);
                    EventAggregator.GetEvent<ConnectionRemovedEvent>().Publish(connection.LibraryId);
                }
            }
        }

        /// <summary>
        /// Synchronizes the data between the library owner and provided user.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        /// <param name="user">The user.</param>
        public void SyncLibraryOwnerAndUser(LibraryConnection libraryConnection, User user)
        {
            if (libraryConnection == null || libraryConnection.Library.Owner == null || user == null || libraryConnection.OwnerId != user.PersonId)
                return;

            if (libraryConnection.LastConnected < libraryConnection.Library.OwnerLastConnected)
            {
                user.Name = libraryConnection.Library.Owner.Name;
                user.Birthday = libraryConnection.Library.Owner.BornOn;
                user.ProfileImagePath = libraryConnection.Library.Owner.ProfileImage.FilePath;
                user.Twitter = libraryConnection.Library.Owner.Twitter;
            }
            else if (libraryConnection.Library.OwnerLastConnected < libraryConnection.LastConnected)
            {
                libraryConnection.Library.Owner.Name = user.Name;
                libraryConnection.Library.Owner.BornOn = user.Birthday;
                libraryConnection.Library.Owner.SetProfileImage(user.ProfileImagePath);
                libraryConnection.Library.Owner.Twitter = user.Twitter;
                libraryConnection.SaveChanges();
            }
        }
    }
}
