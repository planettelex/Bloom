using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a person.
    /// </summary>
    [Table(Name = "person")]
    public class Person
    {
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the person's name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date the person was born.
        /// </summary>
        [Column(Name = "born")]
        public DateTime Born { get; set; }

        /// <summary>
        /// Gets or sets the date the person died.
        /// </summary>
        [Column(Name = "died")]
        public DateTime? Died { get; set; }

        /// <summary>
        /// Gets or sets the person's bio.
        /// </summary>
        [Column(Name = "bio")]
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the person's Twitter.
        /// </summary>
        [Column(Name = "twitter")]
        public string Twitter { get; set; }

        /// <summary>
        /// Gets or sets the person's photos.
        /// </summary>
        public List<PersonPhoto> Photos { get; set; }

        /// <summary>
        /// Gets or sets artists this person is a member of.
        /// </summary>
        public List<ArtistMember> MemberOf { get; set; }

        /// <summary>
        /// Gets or sets the person references.
        /// </summary>
        public List<PersonReference> References { get; set; }
    }
}
