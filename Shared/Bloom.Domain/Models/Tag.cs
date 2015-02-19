using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a tag.
    /// </summary>
    [Table(Name = "tag")]
    public class Tag
    {
        /// <summary>
        /// Creates a new tag instance.
        /// </summary>
        /// <param name="name">The tag name.</param>
        public static Tag Create(string name)
        {
            return new Tag
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the tag identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the tag name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }
    }
}
