using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bloom.Data;
using Bloom.Data.Repositories;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Unity;

namespace Bloom.Services
{
    public class LibraryService : ILibraryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryService" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="libraryRepository">The library repository.</param>
        public LibraryService(IUnityContainer container, ILibraryConnectionRepository libraryConnectionRepository, ILibraryRepository libraryRepository)
        {
            _container = container;
            _libraryConnectionRepository = libraryConnectionRepository;
            _libraryRepository = libraryRepository;            
        }
        private readonly IUnityContainer _container;
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;
        private readonly ILibraryRepository _libraryRepository;

        public LibraryConnection GetLibraryConnection(Guid libraryId)
        {
            var libraryConnection = _libraryConnectionRepository.GetLibraryConnection(libraryId);
            if (libraryConnection == null)
                return null;

            return libraryConnection;
        }

        public LibraryConnection GetLibraryConnection(string filePath)
        {
            var libraryConnection = _libraryConnectionRepository.GetLibraryConnection(filePath);
            if (libraryConnection == null)
                return null;

            return libraryConnection;
        }

        public List<LibraryConnection> ListLibraryConnections()
        {
            var connected = _libraryConnectionRepository.ListLibraryConnections(true);
            var disconnected = _libraryConnectionRepository.ListLibraryConnections(false);

            var allConnections = connected ?? new List<LibraryConnection>();
            allConnections.AddRange(disconnected);

            return allConnections.OrderBy(connection => connection.LibraryName).ToList();
        }

        public bool ConnectLibrary(LibraryConnection libraryConnection, User user, bool timestamp, bool setLibrary)
        {
            if (libraryConnection == null)
                return false;

            if (libraryConnection.IsConnected && libraryConnection.DataSource != null && libraryConnection.DataSource.IsConnected())
                return true;

            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(libraryConnection.FilePath))
                throw new NullReferenceException("Library connection file path cannot be null.");

            if (!File.Exists(libraryConnection.FilePath))
                libraryConnection.IsConnected = false;
            else
            {
                if (libraryConnection.DataSource == null)
                    libraryConnection.DataSource = new LibraryDataSource(_container);

                libraryConnection.DataSource.Connect(libraryConnection.FilePath);
                libraryConnection.IsConnected = true;
                libraryConnection.LastConnectionBy = user.PersonId;

                if (setLibrary)
                    libraryConnection.Library = _libraryRepository.GetLibrary(libraryConnection.DataSource);

                if (timestamp)
                {
                    libraryConnection.LastConnected = DateTime.Now;
                    if (libraryConnection.Library != null && libraryConnection.Library.OwnerId == user.PersonId)
                        libraryConnection.Library.OwnerLastConnected = libraryConnection.LastConnected;
                }
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

        public void RemoveLibraryConnection(LibraryConnection libraryConnection)
        {
            _libraryConnectionRepository.DeleteLibraryConnection(libraryConnection);
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
