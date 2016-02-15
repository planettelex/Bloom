using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with a mood.
    /// </summary>
    [Table(Name = "album_mood")]
    public class AlbumMood
    {
        /// <summary>
        /// Creates a new album mood instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="mood">The mood.</param>
        public static AlbumMood Create(Album album, Mood mood)
        {
            return new AlbumMood
            {
                AlbumId = album.Id,
                MoodId = mood.Id
            };
        }

        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id", IsPrimaryKey = true)]
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the mood identifier.
        /// </summary>
        [Column(Name = "mood_id", IsPrimaryKey = true)]
        public Guid MoodId { get; set; }
    }
}
