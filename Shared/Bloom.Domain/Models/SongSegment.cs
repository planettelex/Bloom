using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a segment of a song.
    /// </summary>
    [Table(Name = "song_segment")]
    public class SongSegment
    {
        /// <summary>
        /// Creates a new song segment instance.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="startTime">The segment start time.</param>
        /// <param name="stopTime">The segment stop time.</param>
        public static SongSegment Create(Song song, int startTime, int stopTime)
        {
            return new SongSegment
            {
                SongId = song.Id,
                StartTime = startTime,
                StopTime = stopTime
            };
        }

        /// <summary>
        /// Gets or sets the song segment identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id")]
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the song segment start time in milliseconds.
        /// </summary>
        [Column(Name = "start_time")]
        public int StartTime { get; set; }

        /// <summary>
        /// Gets or sets the song segment stop time in milliseconds.
        /// </summary>
        [Column(Name = "stop_time")]
        public int StopTime { get; set; }

        /// <summary>
        /// Gets or sets the BPM.
        /// </summary>
        [Column(Name = "bpm")]
        public int? Bpm { get; set; }

        /// <summary>
        /// Gets or sets the song segment musical key.
        /// </summary>
        [Column(Name = "key")]
        public MusicalKeys? Key { get; set; }

        /// <summary>
        /// Gets or sets the time signature identifier.
        /// </summary>
        [Column(Name = "time_signature_id")]
        public Guid TimeSignatureId { get; set; }

        /// <summary>
        /// Gets or sets the song segment time signature.
        /// </summary>
        public TimeSignature TimeSignature { get; set; }
    }
}