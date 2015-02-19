using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an artist with a person.
    /// </summary>
    [Table(Name = "artist_member")]
    public class ArtistMember
    {
        /// <summary>
        /// Creates a new album member instance.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <param name="person">The person.</param>
        public static ArtistMember Create(Artist artist, Person person)
        {
            return new ArtistMember
            {
                Id = Guid.NewGuid(),
                ArtistId = artist.Id,
                Artist = artist,
                PersonId = person.Id,
                Person = person
            };
        }

        /// <summary>
        /// Gets or sets the artist member identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        [Column(Name = "artist_id")]
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        [Column(Name = "person_id")]
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets the date this artist member started with this artist.
        /// </summary>
        [Column(Name = "started")]
        public DateTime? Started { get; set; }

        /// <summary>
        /// Gets or sets the date this artist memeber ended with this artist.
        /// </summary>
        [Column(Name = "ended")]
        public DateTime? Ended { get; set; }

        /// <summary>
        /// Gets or sets the artist member roles.
        /// </summary>
        public List<ArtistMemberRole> Roles { get; set; }

        /// <summary>
        /// Adds a role to this album credit.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <exception cref="System.ArgumentNullException">role</exception>
        public void AddRole(Role role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            if (Roles == null)
                Roles = new List<ArtistMemberRole>();

            Roles.Add(ArtistMemberRole.Create(this, role));
        }
    }
}
