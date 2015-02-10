using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a label personel with a role.
    /// </summary>
    [Table(Name = "label_personel_role")]
    public class LabelPersonelRole
    {
        /// <summary>
        /// Gets or sets the label personel identifier.
        /// </summary>
        [Column(Name = "label_personel_id", IsPrimaryKey = true)]
        public Guid LabelPersonelId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        [Column(Name = "role_id", IsPrimaryKey = true)]
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public Role Role { get; set; }
    }
}
