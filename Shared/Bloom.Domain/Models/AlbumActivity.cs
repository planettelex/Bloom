using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an activity with an album.
    /// </summary>
    [Table(Name = "album_activity")]
    public class AlbumActivity
    {
        /// <summary>
        /// Creates a new album activity instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="activity">The activity.</param>
        public static AlbumActivity Create(Album album, Activity activity)
        {
            return new AlbumActivity
            {
                AlbumId = album.Id,
                ActivityId = activity.Id
            };
        }

        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id", IsPrimaryKey = true)]
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the activity identifier.
        /// </summary>
        [Column(Name = "activity_id", IsPrimaryKey = true)]
        public Guid ActivityId { get; set; }
    }
}
