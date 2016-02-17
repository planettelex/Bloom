using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a role and an album credit.
    /// </summary>
    [Table(Name = "album_credit_role")]
    public class AlbumCreditRole
    {
        /// <summary>
        /// Creates a new album credit role instance.
        /// </summary>
        /// <param name="albumCredit">An album credit.</param>
        /// <param name="role">A role.</param>
        public static AlbumCreditRole Create(AlbumCredit albumCredit, Role role)
        {
            return new AlbumCreditRole
            {
                AlbumCreditId = albumCredit.Id,
                RoleId = role.Id
            };
        }
        
        /// <summary>
        /// Gets or sets the album credit identifier.
        /// </summary>
        [Column(Name = "album_credit_id", IsPrimaryKey = true)]
        public Guid AlbumCreditId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        [Column(Name = "role_id", IsPrimaryKey = true)]
        public Guid RoleId { get; set; }
    }
}
