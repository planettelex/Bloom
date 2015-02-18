using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with a tag.
    /// </summary>
    [Table(Name = "album_tag")]
    public class AlbumTag
    {
        /// <summary>
        /// Creates  a new album tag instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="tag">The tag.</param>
        public static AlbumTag Create(Album album, Tag tag)
        {
            return new AlbumTag
            {
                AlbumId = album.Id,
                Album = album,
                TagId = tag.Id,
                Tag = tag
            };
        }

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
        /// Gets or sets the tag identifier.
        /// </summary>
        [Column(Name = "tag_id", IsPrimaryKey = true)]
        public Guid TagId { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public Tag Tag { get; set; }
    }
}
