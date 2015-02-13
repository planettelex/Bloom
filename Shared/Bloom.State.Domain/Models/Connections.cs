using System;
using System.Collections.Generic;
using System.IO;
using Bloom.Data;
using Bloom.Data.Interfaces;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// The data connections.
    /// </summary>
    public class Connections
    {
        public Connections()
        {
            StateConnection = new StateConnection();
            LibraryConnections = new List<LibraryConnection>();
            LibraryDataSources = new Dictionary<Guid, IDataSource>();
        }

        /// <summary>
        /// Gets or sets the state data connection.
        /// </summary>
        public StateConnection StateConnection { get; set; }

        /// <summary>
        /// Gets or sets the library data connections.
        /// </summary>
        public List<LibraryConnection> LibraryConnections { get; set; }

        /// <summary>
        /// Gets or sets the library data sources.
        /// </summary>
        public Dictionary<Guid, IDataSource> LibraryDataSources { get; set; }

        /// <summary>
        /// Connects the library data sources.
        /// </summary>
        public void ConnectLibraryDataSources()
        {
            if (LibraryConnections == null || LibraryConnections.Count == 0)
                return;

            LibraryDataSources.Clear();
            foreach (var libraryConnection in LibraryConnections)
            {
                if (File.Exists(libraryConnection.FilePath) && libraryConnection.IsConnected)
                    AddLibraryDataSource(libraryConnection);
            }
        }

        public void AddLibraryConnection(LibraryConnection libraryConnection)
        {
            if (LibraryConnections == null)
                return;

            if (!LibraryConnections.Contains(libraryConnection))
                LibraryConnections.Add(libraryConnection);

            if (File.Exists(libraryConnection.FilePath) && libraryConnection.IsConnected)
                AddLibraryDataSource(libraryConnection);
        }

        public void ConnectLibraryDataSource(LibraryConnection libraryConnection)
        {
            if (LibraryConnections == null)
                return;

            libraryConnection.IsConnected = true;
            if (!LibraryConnections.Contains(libraryConnection))
                LibraryConnections.Add(libraryConnection);

            if (File.Exists(libraryConnection.FilePath))
                AddLibraryDataSource(libraryConnection);
        }

        private void AddLibraryDataSource(LibraryConnection libraryConnection)
        {
            var libraryDataSource = new LibraryDataSource();
            libraryDataSource.Connect(libraryConnection.FilePath);
            libraryDataSource.RegisterRepositories();

            LibraryDataSources.Add(libraryConnection.LibraryId, libraryDataSource);
        }
    }
}
