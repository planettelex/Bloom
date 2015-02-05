using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a music playlist.
    /// </summary>
    public class Playlist
    {
        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the playlist name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the playlist description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the playlist length, in milliseconds.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the date this playlist was created on.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets library owner identifier who created the playlist.
        /// </summary>
        public Guid CreatedById { get; set; }

        /// <summary>
        /// Gets or sets library owner who created the playlist.
        /// </summary>
        public Person CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the playlist tracks.
        /// </summary>
        public List<PlaylistTrack> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the playlist artwork.
        /// </summary>
        public List<PlaylistArtwork> Artwork { get; set; }

        /// <summary>
        /// Gets or sets the playlist references.
        /// </summary>
        public List<PlaylistReference> References { get; set; } 
    }
}
