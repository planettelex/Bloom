using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a song with a reference.
    /// </summary>
    [Table(Name = "song_reference")]
    public class SongReference
    {
        /// <summary>
        /// Creates a new song reference instance.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="reference">The reference.</param>
        public static SongReference Create(Song song, Reference reference)
        {
            return new SongReference
            {
                SongId = song.Id,
                ReferenceId = reference.Id
            };
        }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id", IsPrimaryKey = true)]
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        [Column(Name = "reference_id", IsPrimaryKey = true)]
        public Guid ReferenceId { get; set; }
    }
}
