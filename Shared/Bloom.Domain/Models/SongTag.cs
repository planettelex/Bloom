using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a song with a tag.
    /// </summary>
    [Table(Name = "song_tag")]
    public class SongTag
    {
        /// <summary>
        /// Creates a new song tag instance.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="tag">The tag.</param>
        public static SongTag Create(Song song, Tag tag)
        {
            return new SongTag
            {
                SongId = song.Id,
                Song = song,
                TagId = tag.Id,
                Tag = tag
            };
        }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id", IsPrimaryKey = true)]
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the song.
        /// </summary>
        public Song Song { get; set; }

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
