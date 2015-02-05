using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a reference to external information.
    /// </summary>
    public class Reference
    {
        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the reference name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the reference icon URL.
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// Gets or sets the reference URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the reference title.
        /// </summary>
        public string Title { get; set; } 
    }
}
