using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a person.
    /// </summary>
    [Table(Name = "person")]
    public class Person : BindableBase
    {
        /// <summary>
        /// Creates a new person instance.
        /// </summary>
        /// <param name="name">The person's name.</param>
        public static Person Create(string name)
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                Name = name
            };
            return person;
        }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the person's name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;

        /// <summary>
        /// Gets or sets the date the person was born.
        /// </summary>
        [Column(Name = "born_on")]
        public DateTime? BornOn { get; set; }

        /// <summary>
        /// Gets or sets the date the person died.
        /// </summary>
        [Column(Name = "died_on")]
        public DateTime? DiedOn { get; set; }

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

        #region AddPhoto

        /// <summary>
        /// Creates and adds a photo for this person.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns>A new person photo.</returns>
        /// <exception cref="System.ArgumentNullException">photo</exception>
        public PersonPhoto AddPhoto(Photo photo)
        {
            if (photo == null)
                throw new ArgumentNullException("photo");

            if (Photos == null)
                Photos = new List<PersonPhoto>();

            var highestPriority = Photos.Any() ? Photos.Max(pic => pic.Priority) : 0;
            var nextPriority = highestPriority + 1;
            var personPhoto = PersonPhoto.Create(this, photo, nextPriority);
            Photos.Add(personPhoto);

            return personPhoto;
        }

        /// <summary>
        /// Creates and adds a photo for this person.
        /// </summary>
        /// <param name="url">The photo URL.</param>
        /// <returns>A new person photo.</returns>
        /// <exception cref="System.ArgumentNullException">photo</exception>
        public PersonPhoto AddPhoto(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            if (Photos == null)
                Photos = new List<PersonPhoto>();

            var highestPriority = Photos.Any() ? Photos.Max(pic => pic.Priority) : 0;
            var nextPriority = highestPriority + 1;
            var photo = Photo.Create(url);
            var personPhoto = PersonPhoto.Create(this, photo, nextPriority);
            Photos.Add(personPhoto);

            return personPhoto;
        }

        #endregion

        /// <summary>
        /// Gets or sets artists this person is a member of.
        /// </summary>
        public List<ArtistMember> MemberOf { get; set; }

        #region AddMemberOf

        /// <summary>
        /// Creates and adds an artist this person is member of.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>A new artist member.</returns>
        /// <exception cref="System.ArgumentNullException">artist</exception>
        public ArtistMember AddMemberOf(Artist artist)
        {
            if (artist == null)
                throw new ArgumentNullException("artist");

            if (MemberOf == null)
                MemberOf = new List<ArtistMember>();

            var artistMember = ArtistMember.Create(artist, this);
            MemberOf.Add(artistMember);

            return artistMember;
        }

        #endregion

        /// <summary>
        /// Gets or sets the person references.
        /// </summary>
        public List<PersonReference> References { get; set; }

        #region AddReference

        /// <summary>
        /// Creates and adds a reference to this person.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <returns>A new person reference.</returns>
        /// <exception cref="System.ArgumentNullException">reference</exception>
        public PersonReference AddReference(Reference reference)
        {
            if (reference == null)
                throw new ArgumentNullException("reference");

            if (References == null)
                References = new List<PersonReference>();

            var personReference = PersonReference.Create(this, reference);
            References.Add(personReference);

            return personReference;
        }

        #endregion
    }
}
