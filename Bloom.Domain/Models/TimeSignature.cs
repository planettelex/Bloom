using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a time signature.
    /// </summary>
    public class TimeSignature
    {
        /// <summary>
        /// Gets or sets the time signature beats.
        /// </summary>
        public int Beats { get; set; }

        /// <summary>
        /// Gets or sets the length of each note in the time signature.
        /// </summary>
        public NoteLength NoteLength { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Beats + "/" + (int)NoteLength;
        }
    }
}
