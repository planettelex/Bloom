using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a music genre.
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Gets or sets the genre identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the genre name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the genre description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the parent genre identifier.
        /// </summary>
        public Guid ParentGenreId { get; set; }

        /// <summary>
        /// Gets or sets the parent genre.
        /// </summary>
        public Genre ParentGenre { get; set; }
    }
}
