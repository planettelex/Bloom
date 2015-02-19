using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associcates a library with an album.
    /// </summary>
    [Table(Name = "library_album")]
    public class LibraryAlbum
    {
        /// <summary>
        /// Creates a new library album instance.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="album">The album.</param>
        public static LibraryAlbum Create(Library library, Album album)
        {
            return new LibraryAlbum
            {
                LibraryId = library.Id,
                AlbumId = album.Id,
                Album = album
            };
        }

        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        public Guid LibraryId { get; set; }
        
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
        public int? Rating { get; set; }

        /// <summary>
        /// Gets or sets the library album review.
        /// </summary>
        [Column(Name = "review")]
        public string Review { get; set; }

        /// <summary>
        /// Gets or sets the album media included in this library.
        /// </summary>
        public List<LibraryAlbumMedia> Media { get; set; }

        #region AddMedia

        /// <summary>
        /// Creates and adds a media to this library album.
        /// </summary>
        /// <param name="mediaType">The type of media.</param>
        /// <returns>A new library album media.</returns>
        public LibraryAlbumMedia AddMedia(MediaTypes mediaType)
        {
            if (Media == null)
                Media = new List<LibraryAlbumMedia>();

            var libraryAlbumMedia = LibraryAlbumMedia.Create(this, mediaType);
            Media.Add(libraryAlbumMedia);

            return libraryAlbumMedia;
        }

        /// <summary>
        /// Creates and adds a media to this library album.
        /// </summary>
        /// <param name="mediaType">The type of media.</param>
        /// <param name="release">The album release of the media.</param>
        /// <returns>A new library album media.</returns>
        public LibraryAlbumMedia AddMedia(MediaTypes mediaType, AlbumRelease release)
        {
            if (Media == null)
                Media = new List<LibraryAlbumMedia>();

            var libraryAlbumMedia = LibraryAlbumMedia.Create(this, mediaType, release);
            Media.Add(libraryAlbumMedia);

            return libraryAlbumMedia;
        }

        #endregion
    }
}
