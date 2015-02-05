using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a person with a reference.
    /// </summary>
    public class PersonReference
    {
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        public Guid ReferenceId { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        public Reference Reference { get; set; }
    }
}
