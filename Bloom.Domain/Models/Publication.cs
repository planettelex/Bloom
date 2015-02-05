using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a publication.
    /// </summary>
    public class Publication
    {
        /// <summary>
        /// Gets or sets the publication identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the publication name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the publication website URL.
        /// </summary>
        public string WebsiteUrl { get; set; }
    }
}
