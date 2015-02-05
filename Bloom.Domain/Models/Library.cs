using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a music library.
    /// </summary>
    public class Library
    {
        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the library file path.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the library name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the library description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the identifer owner.
        /// </summary>
        public Person Owner { get; set; }
    }
}
