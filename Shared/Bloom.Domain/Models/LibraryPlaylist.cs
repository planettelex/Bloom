using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a library with a playlist.
    /// </summary>
    [Table(Name = "library_playlist")]
    public class LibraryPlaylist
    {
        /// <summary>
        /// Creates a new library playlist instance.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="playlist">The playlist.</param>
        public static LibraryPlaylist Create(Library library, Playlist playlist)
        {
            return new LibraryPlaylist
            {
                LibraryId = library.Id,
                PlaylistId = playlist.Id,
                Playlist = playlist
            };
        }

        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        public Guid LibraryId { get; set; }

        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        [Column(Name = "playlist_id", IsPrimaryKey = true)]
        public Guid PlaylistId { get; set; }

        /// <summary>
        /// Gets or sets the playlist.
        /// </summary>
        public Playlist Playlist { get; set; }

        /// <summary>
        /// Gets or sets the playlist rating.
        /// </summary>
        [Column(Name = "rating")]
        public int? Rating { get; set; }
    }
}
