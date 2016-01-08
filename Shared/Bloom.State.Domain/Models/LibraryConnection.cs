using System;
using System.Data.Linq.Mapping;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Bloom.Data;
using Bloom.Domain.Models;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// A connection to a library database.
    /// </summary>
    [Table(Name = "library_connection")]
    public class LibraryConnection : BindableBase
    {
        /// <summary>
        /// Creates a new library connection instance.
        /// </summary>
        /// <param name="library">A library.</param>
        public static LibraryConnection Create(Library library)
        {
            if (library == null || library.Owner == null)
                return null;

            var libraryConnection = new LibraryConnection
            {
                Library = library,
                LibraryId = library.Id,
                LibraryName = library.Name,
                FilePath = library.FilePath,
                OwnerId = library.OwnerId,
                OwnerName = library.Owner.Name,
                LastConnected = DateTime.Now,
                LastConnectionBy = library.OwnerId
            };
            return libraryConnection;
        }

        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        [Column(Name = "library_id", IsPrimaryKey = true)]
        public Guid LibraryId
        {
            get { return _libraryId; }
            set { SetProperty(ref _libraryId, value); }
        }
        private Guid _libraryId;

        /// <summary>
        /// Gets or sets the library name.
        /// </summary>
        [Column(Name = "library_name")]
        public string LibraryName 
        { 
            get { return _libraryName; } 
            set { SetProperty(ref _libraryName, value); } 
        }
        private string _libraryName;

        /// <summary>
        /// Gets or sets the library database file path.
        /// </summary>
        [Column(Name = "file_path")]
        public string FilePath
        {
            get { return _filePath; }
            set { SetProperty(ref _filePath, value); }
        }
        private string _filePath;

        /// <summary>
        /// Gets or sets a value indicating whether this library is connected.
        /// </summary>
        [Column(Name = "is_connected")]
        public bool IsConnected { get; set; }

        /// <summary>
        /// Gets or sets the last connected date and time.
        /// </summary>
        [Column(Name = "last_connected")]
        public DateTime LastConnected { get; set; }

        /// <summary>
        /// Gets or sets the person identifier of the last user to connect.
        /// </summary>
        [Column(Name = "last_connection_by")]
        public Guid LastConnectionBy { get; set; }

        /// <summary>
        /// Gets or sets the owner's person identifier.
        /// </summary>
        [Column(Name = "owner_id")]
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the owner.
        /// </summary>
        [Column(Name = "owner_name")]
        public string OwnerName
        {
            get { return _ownerName; }
            set { SetProperty(ref _ownerName, value); }
        }
        private string _ownerName;

        /// <summary>
        /// Gets or sets the connect button visibility.
        /// </summary>
        public Visibility ConnectButtonVisibility
        {
            get { return _connectButtonVisibility; }
            set { SetProperty(ref _connectButtonVisibility, value); }
        }
        private Visibility _connectButtonVisibility;

        /// <summary>
        /// Gets or sets the disconnect button visibility.
        /// </summary>
        public Visibility DisconnectButtonVisibility
        {
            get { return _disconnectButtonVisibility; }
            set { SetProperty(ref _disconnectButtonVisibility, value); }
        }
        private Visibility _disconnectButtonVisibility;

        /// <summary>
        /// Gets or sets the file missing button visibility.
        /// </summary>
        public Visibility FileMissingButtonVisibility
        {
            get { return _fileMissingButtonVisibility; }
            set { SetProperty(ref _fileMissingButtonVisibility, value); }
        }
        private Visibility _fileMissingButtonVisibility;

        /// <summary>
        /// Gets or sets the background brush.
        /// </summary>
        public Brush BackgroundBrush
        {
            get { return _backgroundBrush; }
            set { SetProperty(ref _backgroundBrush, value); }
        }
        private Brush _backgroundBrush;

        /// <summary>
        /// Gets or sets the connected library.
        /// </summary>
        public Library Library { get; set; }

        /// <summary>
        /// Gets or sets the library data source.
        /// </summary>
        public LibraryDataSource DataSource { get; set; }

        /// <summary>
        /// Saves the connected library data source.
        /// </summary>
        public void SaveChanges()
        {
            DataSource.Save();
        }

        /// <summary>
        /// Disconnects the data source.
        /// </summary>
        public void Disconnect()
        {
            DataSource.Disconnect();
            IsConnected = false;
        }

        /// <summary>
        /// Sets the button visibilities.
        /// </summary>
        /// <exception cref="System.NullReferenceException">File path cannot be null or empty.</exception>
        public void SetButtonVisibilities()
        {
            if (string.IsNullOrEmpty(FilePath))
                throw new NullReferenceException("File path cannot be null or empty.");

            if (!File.Exists(FilePath))
            {
                FileMissingButtonVisibility = Visibility.Visible;
                DisconnectButtonVisibility = Visibility.Collapsed;
                ConnectButtonVisibility = Visibility.Collapsed;
            }
            else if (!IsConnected || DataSource == null || !DataSource.IsConnected())
            {
                FileMissingButtonVisibility = Visibility.Collapsed;
                DisconnectButtonVisibility = Visibility.Collapsed;
                ConnectButtonVisibility = Visibility.Visible;
            }
            else
            {
                FileMissingButtonVisibility = Visibility.Collapsed;
                DisconnectButtonVisibility = Visibility.Visible;
                ConnectButtonVisibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Sets the owner.
        /// </summary>
        /// <param name="person">The person.</param>
        public void SetOwner(Person person)
        {
            if (person == null)
                return;

            OwnerId = person.Id;
            OwnerName = person.Name;
            if (Library != null)
                Library.Owner = person;
        }
    }
}
