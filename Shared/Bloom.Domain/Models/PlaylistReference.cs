using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a playlist and a reference.
    /// </summary>
    [Table(Name = "playlist_reference")]
    public class PlaylistReference
    {
        /// <summary>
        /// Creates a new playlist reference instance.
        /// </summary>
        /// <param name="playlist">A playlist.</param>
        /// <param name="reference">The reference.</param>
        public static PlaylistReference Create(Playlist playlist, Reference reference)
        {
            return new PlaylistReference
            {
                PlaylistId = playlist.Id,
                ReferenceId = reference.Id
            };
        }

        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        [Column(Name = "playlist_id", IsPrimaryKey = true)]
        public Guid PlaylistId { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        [Column(Name = "reference_id", IsPrimaryKey = true)]
        public Guid ReferenceId { get; set; }
    }
}
