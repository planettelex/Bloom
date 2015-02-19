using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a publication.
    /// </summary>
    [Table(Name = "publication")]
    public class Publication
    {
        /// <summary>
        /// Creates a new publication instance.
        /// </summary>
        /// <param name="name">The publication name.</param>
        public static Publication Create(string name)
        {
            return new Publication
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the publication identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the publication name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the publication website URL.
        /// </summary>
        [Column(Name = "website_url")]
        public string WebsiteUrl { get; set; }
    }
}
