using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bloom.Common.ExtensionMethods;
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
    public class SharedLibraryService : ISharedLibraryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedLibraryService" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="libraryRepository">The library repository.</param>
        /// <param name="personRepository">The person repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public SharedLibraryService(IUnityContainer container, IEventAggregator eventAggregator, IRegionManager regionManager,
            ILibraryConnectionRepository libraryConnectionRepository, ILibraryRepository libraryRepository, IPersonRepository personRepository, IUserRepository userRepository)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _libraryConnectionRepository = libraryConnectionRepository;
            _personRepository = personRepository;
            _libraryRepository = libraryRepository;
            _userRepository = userRepository;

            // Subscribe to events
            _eventAggregator.GetEvent<CreateNewLibraryEvent>().Subscribe(CreateNewLibrary);
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
            
        }
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRegionManager _regionManager;
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public ApplicationState State { get; private set; }

        private void SetState(object nothing)
        {
            State = (ApplicationState) _regionManager.Regions[Common.Settings.MenuRegion].Context;
        }

        public void CreateNewLibrary(Library library)
        {
            if (library == null)
                throw new ArgumentNullException("library");

            var dataSource = new LibraryDataSource(_container);
            dataSource.Create(library.FilePath);
            var libraryConnection = LibraryConnection.Create(library);
            ConnectLibrary(libraryConnection, State.User, true, false);
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
            
            var success = ConnectLibrary(libraryConnection, State.User, true, true);
            if (!success)
                return null;

            var existingUser = _userRepository.GetUser(library.OwnerId);
            if (existingUser == null)
                _userRepository.AddUser(User.Create(library.Owner));
            else
                SyncLibraryOwnerAndUser(libraryConnection, existingUser);

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
                    State.SetUser(user);
                    userCreated = true;
                }

                if (user == null)
                    throw new NullReferenceException("User cannot be null.");

                if (userCreated)
                    _eventAggregator.GetEvent<UserChangedEvent>().Publish(null);

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

        public List<LibraryConnection> ListLibraryConnections()
        {
            var connected = _libraryConnectionRepository.ListLibraryConnections(true);
            var disconnected = _libraryConnectionRepository.ListLibraryConnections(false);

            var allConnections = connected ?? new List<LibraryConnection>();
            allConnections.AddRange(disconnected);

            return allConnections.OrderBy(connection => connection.LibraryName).ToList();
        }

        public void RemoveLibraryConnection(LibraryConnection libraryConnection)
        {
            _libraryConnectionRepository.DeleteLibraryConnection(libraryConnection);
        }

        public void ResetLibraryConnections()
        {
            if (State == null)
                return;

            if (State.Connections == null)
                State.Connections = new List<LibraryConnection>();

            var connectedLibraries = _libraryConnectionRepository.ListLibraryConnections(true);
            foreach (var libraryConnection in connectedLibraries)
            {
                if (!State.Connections.Contains(libraryConnection))
                {
                    ConnectLibrary(libraryConnection, State.User, false, true);
                    _eventAggregator.GetEvent<ConnectionAddedEvent>().Publish(libraryConnection);
                }
            }


            var disconnectedLibraries = _libraryConnectionRepository.ListLibraryConnections(false);
            foreach (var libraryConnection in disconnectedLibraries)
                if (State.Connections.Contains(libraryConnection))
                    _eventAggregator.GetEvent<ConnectionRemovedEvent>().Publish(libraryConnection.LibraryId);

            State.Connections = new List<LibraryConnection>();
            State.Connections.AddRange(connectedLibraries);
        }

        public void SyncLibraryOwnerAndUser(LibraryConnection libraryConnection, User user)
        {
            if (libraryConnection == null || libraryConnection.Library.Owner == null || user == null || libraryConnection.OwnerId != user.PersonId)
                return;

            if (libraryConnection.LastConnected < libraryConnection.Library.OwnerLastConnected)
            {
                user.Name = libraryConnection.Library.Owner.Name;
                user.Birthday = libraryConnection.Library.Owner.BornOn;
                user.ProfileImageUrl = libraryConnection.Library.Owner.ProfileImageUrl;
                user.Twitter = libraryConnection.Library.Owner.Twitter;
            }
            else if (libraryConnection.Library.OwnerLastConnected < libraryConnection.LastConnected)
            {
                libraryConnection.Library.Owner.Name = user.Name;
                libraryConnection.Library.Owner.BornOn = user.Birthday;
                libraryConnection.Library.Owner.ProfileImageUrl = user.ProfileImageUrl;
                libraryConnection.Library.Owner.Twitter = user.Twitter;
                libraryConnection.SaveChanges();
            }
        }
    }
}
