using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the person's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date the person was born.
        /// </summary>
        public DateTime Born { get; set; }

        /// <summary>
        /// Gets or sets the date the person died.
        /// </summary>
        public DateTime? Died { get; set; }

        /// <summary>
        /// Gets or sets the person's bio.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the person's Twitter.
        /// </summary>
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
