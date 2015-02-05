using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a role a person may have.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the role name.
        /// </summary>
        public string Name { get; set; }
    }
}
