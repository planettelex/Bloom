using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a playlist and a tag.
    /// </summary>
    [Table(Name = "playlist_tag")]
    public class PlaylistTag
    {
        /// <summary>
        /// Creates a new playlist tag instance.
        /// </summary>
        /// <param name="playlist">The playlist.</param>
        /// <param name="tag">The tag.</param>
        public static PlaylistTag Create(Playlist playlist, Tag tag)
        {
            return new PlaylistTag
            {
                PlaylistId = playlist.Id,
                TagId = tag.Id
            };
        }

        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        [Column(Name = "playlist_id", IsPrimaryKey = true)]
        public Guid PlaylistId { get; set; }

        /// <summary>
        /// Gets or sets the tag identifier.
        /// </summary>
        [Column(Name = "tag_id", IsPrimaryKey = true)]
        public Guid TagId { get; set; }
    }
}
