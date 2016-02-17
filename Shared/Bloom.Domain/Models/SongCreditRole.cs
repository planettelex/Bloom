using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a role and a song credit.
    /// </summary>
    [Table(Name = "song_credit_role")]
    public class SongCreditRole
    {
        /// <summary>
        /// Creates a new song credit role instance.
        /// </summary>
        /// <param name="songCredit">The song credit.</param>
        /// <param name="role">The role.</param>
        public static SongCreditRole Create(SongCredit songCredit, Role role)
        {
            return new SongCreditRole
            {
                SongCreditId = songCredit.Id,
                RoleId = role.Id
            };
        }

        /// <summary>
        /// Gets or sets the song credit identifier.
        /// </summary>
        [Column(Name = "song_credit_id", IsPrimaryKey = true)]
        public Guid SongCreditId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        [Column(Name = "role_id", IsPrimaryKey = true)]
        public Guid RoleId { get; set; }
    }
}
