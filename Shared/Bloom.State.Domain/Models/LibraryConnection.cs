using System;
using System.Data.Linq.Mapping;
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
            if (library == null)
                return null;

            var libraryConnection = new LibraryConnection
            {
                Library = library,
                LibraryId = library.Id,
                LibraryName = library.Name,
                FilePath = library.FilePath,
                IsConnected = true,
                LastConnected = DateTime.Now,
                LastConnectionBy = library.OwnerId
            };
            return libraryConnection;
        }

        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        [Column(Name = "library_id", IsPrimaryKey = true)]
        public Guid LibraryId { get; set; }

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
    }
}
