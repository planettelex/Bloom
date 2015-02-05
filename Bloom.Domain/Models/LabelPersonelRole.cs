using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a label personel with a role.
    /// </summary>
    public class LabelPersonelRole
    {
        /// <summary>
        /// Gets or sets the label personel identifier.
        /// </summary>
        public Guid LabelPersonelId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public Role Role { get; set; }
    }
}
