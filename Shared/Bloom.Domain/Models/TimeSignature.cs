using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a time signature.
    /// </summary>
    [Table(Name = "time_signature")]
    public class TimeSignature
    {
        /// <summary>
        /// Creates a new time signature instance.
        /// </summary>
        /// <param name="beats">The number of beats per measure.</param>
        /// <param name="noteLength">The length of each beat.</param>
        public static TimeSignature Create(int beats, NoteLength noteLength)
        {
            return new TimeSignature
            {
                Id = Guid.NewGuid(),
                Beats = beats,
                NoteLength = noteLength
            };
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the time signature beats.
        /// </summary>
        [Column(Name = "beats")]
        public int Beats { get; set; }

        /// <summary>
        /// Gets or sets the length of each note in the time signature.
        /// </summary>
        [Column(Name = "note_length")]
        public NoteLength NoteLength { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Beats + "/" + (int) NoteLength;
        }
    }
}
