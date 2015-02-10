using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a song credit with a role.
    /// </summary>
    [Table(Name = "song_credit_role")]
    public class SongCreditRole
    {
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

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public Role Role { get; set; }
    }
}
