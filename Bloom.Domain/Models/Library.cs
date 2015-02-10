using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a music library.
    /// </summary>
    [Table(Name = "library")]
    public class Library
    {
        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the library name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the library description.
        /// </summary>
        [Column(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        [Column(Name = "owner_id")]
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the identifer owner.
        /// </summary>
        public Person Owner { get; set; }
    }
}
