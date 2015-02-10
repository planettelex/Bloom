using System;
using System.Data.Linq.Mapping;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// A connection to a library database.
    /// </summary>
    [Table(Name = "library_connection")]
    public class LibraryConnection
    {
        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        [Column(Name = "library_id", IsPrimaryKey = true)]
        public Guid LibraryId { get; set; }

        /// <summary>
        /// Gets or sets the library name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the library file path.
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
