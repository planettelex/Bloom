using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a music genre.
    /// </summary>
    [Table(Name = "genre")]
    public class Genre
    {
        /// <summary>
        /// Gets or sets the genre identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the genre name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the genre description.
        /// </summary>
        [Column(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the parent genre identifier.
        /// </summary>
        [Column(Name = "parent_genre_id")]
        public Guid ParentGenreId { get; set; }

        /// <summary>
        /// Gets or sets the parent genre.
        /// </summary>
        public Genre ParentGenre { get; set; }
    }
}
