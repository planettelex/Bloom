using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a role with an album credit.
    /// </summary>
    [Table(Name = "album_credit_role")]
    public class AlbumCreditRole
    {
        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        [Column(Name = "role_id", IsPrimaryKey = true)]
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Gets or sets the album credit identifier.
        /// </summary>
        [Column(Name = "album_credit_id", IsPrimaryKey = true)]
        public Guid AlbumCreditId { get; set; }
    }
}
