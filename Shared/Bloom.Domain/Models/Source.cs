using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a reference or review source.
    /// </summary>
    [Table(Name = "source")]
    public class Source
    {
        /// <summary>
        /// Creates a new publication instance.
        /// </summary>
        /// <param name="name">The publication name.</param>
        public static Source Create(string name)
        {
            return new Source
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the source identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the source name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the source website URL.
        /// </summary>
        [Column(Name = "website_url")]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [Column(Name = "type")]
        public SourceType Type { get; set; }
    }
}
