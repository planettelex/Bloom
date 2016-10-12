using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a song and a person.
    /// </summary>
    [Table(Name = "song_credit")]
    public class SongCredit
    {
        /// <summary>
        /// Creates a new song credit instance.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="person">The person credited.</param>
        public static SongCredit Create(Song song, Person person)
        {
            return new SongCredit
            {
                Id = Guid.NewGuid(),
                SongId = song.Id,
                Person = person,
                Roles = new List<Role>()
            };
        }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id")]
        public Guid SongId { get; set; }

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
        /// Gets or sets whether this person has a "featured" credit.
        /// </summary>
        [Column(Name = "is_featured")]
        public bool IsFeatured { get; set; }

        /// <summary>
        /// Gets or sets the person's roles on the song.
        /// </summary>
        public List<Role> Roles { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            var personString = Person != null ? Person.Name : PersonId.ToString();
            if (Roles == null)
                return personString;

            var rolesString = Roles.Aggregate(string.Empty, (current, role) => current + (role + ", "));
            return string.Format("{0} ({1})", personString, rolesString.TrimEnd(',', ' '));
        }
    }
}
