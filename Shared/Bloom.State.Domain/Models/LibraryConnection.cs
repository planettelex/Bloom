using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Models;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// A connection to a library database.
    /// </summary>
    [Table(Name = "library_connection")]
    public class LibraryConnection
    {
        /// <summary>
        /// Creates a new library connection instance.
        /// </summary>
        /// <param name="library">A library.</param>
        public static LibraryConnection Create(Library library)
        {
            var libraryConnection = new LibraryConnection
            {
                LibraryId = library.Id,
                LibraryName = library.Name,
                FilePath = library.FilePath,
                OwnerId = library.OwnerId,
                OwnerName = library.Owner.Name,
                IsConnected = true,
                LastConnected = DateTime.Now
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
        public string LibraryName { get; set; }

        /// <summary>
        /// Gets or sets the library database file path.
        /// </summary>
        [Column(Name = "file_path")]
        public string FilePath { get; set; }

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
        /// Gets or sets the owner identifier.
        /// </summary>
        [Column(Name = "owner_id")]
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the owner's name.
        /// </summary>
        [Column(Name = "owner_name")]
        public string OwnerName { get; set; }
    }
}
