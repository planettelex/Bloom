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
        /// Creates a new genre instance.
        /// </summary>
        /// <param name="name">The genre name.</param>
        public static Genre Create(string name)
        {
            return new Genre
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Creates a new genre instance.
        /// </summary>
        /// <param name="name">The genre name.</param>
        /// <param name="parentGenre">The parent genre.</param>
        public static Genre Create(string name, Genre parentGenre)
        {
            return new Genre
            {
                Id = Guid.NewGuid(),
                Name = name,
                ParentGenreId = parentGenre.Id,
                ParentGenre = parentGenre
            };
        }

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
