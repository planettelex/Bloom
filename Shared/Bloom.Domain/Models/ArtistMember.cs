using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an artist member.
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Prism.Mvvm.BindableBase" />
    [Table(Name = "artist_member")]
    public class ArtistMember : BindableBase
    {
        /// <summary>
        /// Creates a new album member instance.
        /// </summary>
        /// <param name="artist">An artist.</param>
        /// <param name="person">A person.</param>
        /// <param name="priority">The member priority.</param>
        public static ArtistMember Create(Artist artist, Person person, int priority)
        {
            return new ArtistMember
            {
                Id = Guid.NewGuid(),
                ArtistId = artist.Id,
                Person = person,
                Priority = priority,
                Roles = new List<Role>()
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
        /// Gets or sets the person identifier.
        /// </summary>
        [Column(Name = "person_id")]
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                PersonId = _person == null ? Guid.Empty : _person.Id;
            }
        }
        private Person _person;

        /// <summary>
        /// Gets or sets the date this artist member started with this artist.
        /// </summary>
        [Column(Name = "started")]
        public DateTime? Started
        {
            get { return _started; }
            set { SetProperty(ref _started, value); }
        }
        private DateTime? _started;

        /// <summary>
        /// Gets or sets the date this artist memeber ended with this artist.
        /// </summary>
        [Column(Name = "ended")]
        public DateTime? Ended
        {
            get { return _ended; }
            set { SetProperty(ref _ended, value); }
        }
        private DateTime? _ended;

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        [Column(Name = "priority")]
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the artist member roles.
        /// </summary>
        public List<Role> Roles { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Person != null ? Person.Name : PersonId.ToString();
        }
    }
}
