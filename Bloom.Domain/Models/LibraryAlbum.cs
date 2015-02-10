using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associcates a library with an album.
    /// </summary>
    [Table(Name = "library_album")]
    public class LibraryAlbum
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id", IsPrimaryKey = true)]
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the library album rating.
        /// </summary>
        [Column(Name = "rating")]
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the library album review.
        /// </summary>
        [Column(Name = "review")]
        public string Review { get; set; }

        /// <summary>
        /// Gets or sets the album media included in this library.
        /// </summary>
        public List<LibraryAlbumMedia> Media { get; set; }
    }
}
