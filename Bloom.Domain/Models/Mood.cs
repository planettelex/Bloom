using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a mood (e.g. Happy, Excited, Brooding)
    /// </summary>
    public class Mood
    {
        /// <summary>
        /// Gets or sets the mood identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the mood name.
        /// </summary>
        public string Name { get; set; }
    }
}
