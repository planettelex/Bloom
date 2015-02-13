using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a music playlist.
    /// </summary>
    [Table(Name = "playlist")]
    public class Playlist
    {
        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the playlist name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the playlist description.
        /// </summary>
        [Column(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the playlist length, in milliseconds.
        /// </summary>
        [Column(Name = "length")]
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the date this playlist was created on.
        /// </summary>
        [Column(Name = "created_on")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets library owner identifier who created the playlist.
        /// </summary>
        [Column(Name = "created_by_id")]
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
