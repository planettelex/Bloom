using System;
using System.Collections.Generic;
using Bloom.Data;
using Bloom.Data.Repositories;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Unity;

namespace Bloom.Services
{
    public class LibraryService : ILibraryService
    {
        public LibraryService(IUnityContainer container, ILibraryConnectionRepository libraryConnectionRepository, ILibraryRepository libraryRepository)
        {
            _container = container;
            _libraryConnectionRepository = libraryConnectionRepository;
            _libraryRepository = libraryRepository;
        }
        private readonly IUnityContainer _container;
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;
        private readonly ILibraryRepository _libraryRepository;

        public List<LibraryConnection> ListLibraryConnections()
        {
            var connected = _libraryConnectionRepository.ListLibraryConnections(true);
            var disconnected = _libraryConnectionRepository.ListLibraryConnections(false);

            var allConnections = connected ?? new List<LibraryConnection>();
            allConnections.AddRange(disconnected);

            return allConnections;
        }

        public void MakeLibraryConnections(List<LibraryConnection> libraryConnections, User user)
        {
            if (libraryConnections == null || libraryConnections.Count == 0)
                return;

            foreach (var libraryConnection in libraryConnections)
            {
                ConnectLibrary(libraryConnection, user);
                libraryConnection.Library = _libraryRepository.GetLibrary(libraryConnection.DataSource);
            }
        }

        public void ConnectLibrary(LibraryConnection libraryConnection, User user)
        {
            if (libraryConnection == null)
                return;

            if (string.IsNullOrEmpty(libraryConnection.FilePath))
                throw new NullReferenceException("Library connection file path cannot be null.");

            if (user == null)
                throw new ArgumentNullException("user");

            if (libraryConnection.DataSource == null)
                libraryConnection.DataSource = new LibraryDataSource(_container);

            libraryConnection.DataSource.Connect(libraryConnection.FilePath);
            libraryConnection.LastConnected = DateTime.Now;
            libraryConnection.LastConnectionBy = user.PersonId;
        }
    }
}
