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
        /// Gets or sets the library file path.
        /// </summary>
        [Column(Name = "file_path")]
        public string FilePath { get; set; }
    }
}
