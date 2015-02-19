using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a musical artist.
    /// </summary>
    [Table(Name = "artist")]
    public class Artist
    {
        /// <summary>
        /// Creates a new artist instance.
        /// </summary>
        /// <param name="name">The artist name.</param>
        public static Artist Create(string name)
        {
            return new Artist
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }
        
        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the artist name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date this artist started their career.
        /// </summary>
        [Column(Name = "started_on")]
        public DateTime? StartedOn { get; set; }

        /// <summary>
        /// Gets or sets the date this artist ended their career.
        /// </summary>
        [Column(Name = "ended_on")]
        public DateTime? EndedOn { get; set; }

        /// <summary>
        /// Gets or sets the artist's bio.
        /// </summary>
        [Column(Name = "bio")]
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the artist's Twitter handle.
        /// </summary>
        [Column(Name = "twitter")]
        public string Twitter { get; set; }

        /// <summary>
        /// Gets or sets a whether this is a solo artist.
        /// </summary>
        [Column(Name = "is_solo")]
        public bool IsSolo { get; set; }

        /// <summary>
        /// Gets or sets the artist photos.
        /// </summary>
        public List<ArtistPhoto> Photos { get; set; }

        #region AddPhoto

        /// <summary>
        /// Adds a photo to this artist.
        /// </summary>
        /// <param name="photo">The artist photo.</param>
        /// <exception cref="System.ArgumentNullException">photo</exception>
        public void AddPhoto(Photo photo)
        {
            if (photo == null)
                throw new ArgumentNullException("photo");

            if (Photos == null)
                Photos = new List<ArtistPhoto>();

            var highestPriority = Photos.Any() ? Photos.Max(pic => pic.Priority) : 0;
            var nextPriority = highestPriority + 1;

            Photos.Add(ArtistPhoto.Create(this, photo, nextPriority));
        }

        #endregion

        /// <summary>
        /// Gets or sets the artist members.
        /// </summary>
        public List<ArtistMember> Members { get; set; }

        #region AddMember

        public void AddMember(Person person)
        {
            if (person == null)
                throw new ArgumentNullException("person");

            if (Members == null)
                Members = new List<ArtistMember>();

            Members.Add(ArtistMember.Create(this, person));
        }

        public void AddMember(Person person, IList<Role> roles)
        {
            if (person == null)
                throw new ArgumentNullException("person");

            if (roles == null)
                throw new ArgumentNullException("roles");

            if (Members == null)
                Members = new List<ArtistMember>();

            var member = ArtistMember.Create(this, person);
            foreach (var role in roles)
                member.AddRole(role);

            Members.Add(member);
        }

        #endregion

        /// <summary>
        /// Gets or sets the artist references.
        /// </summary>
        public List<ArtistReference> References { get; set; }

        #region AddReference

        /// <summary>
        /// Adds a reference to this artist.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <exception cref="System.ArgumentNullException">reference</exception>
        public void AddReference(Reference reference)
        {
            if (reference == null)
                throw new ArgumentNullException("reference");

            if (References == null)
                References = new List<ArtistReference>();

            References.Add(ArtistReference.Create(this, reference));
        }

        #endregion
    }
}
