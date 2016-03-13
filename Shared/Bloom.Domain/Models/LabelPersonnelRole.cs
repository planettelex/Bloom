using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a label personnel and a role.
    /// </summary>
    [Table(Name = "label_personnel_role")]
    public class LabelPersonnelRole
    {
        /// <summary>
        /// Creates a new label personel role instance.
        /// </summary>
        /// <param name="labelPersonel">A label personnel.</param>
        /// <param name="role">The label personnel's role.</param>
        public static LabelPersonnelRole Create(LabelPersonnel labelPersonel, Role role)
        {
            return new LabelPersonnelRole
            {
                LabelPersonelId = labelPersonel.Id,
                RoleId = role.Id
            };
        }

        /// <summary>
        /// Gets or sets the label personnel identifier.
        /// </summary>
        [Column(Name = "label_personnel_id", IsPrimaryKey = true)]
        public Guid LabelPersonelId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        [Column(Name = "role_id", IsPrimaryKey = true)]
        public Guid RoleId { get; set; }
    }
}
