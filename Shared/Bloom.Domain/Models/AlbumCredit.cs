 using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between an album and a person.
    /// </summary>
    [Table(Name = "album_credit")]
    public class AlbumCredit
    {
        /// <summary>
        /// Creates a new album credit instance.
        /// </summary>
        /// <param name="album">An album.</param>
        /// <param name="person">The person credited.</param>
        public static AlbumCredit Create(Album album, Person person)
        {
            return new AlbumCredit
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                Person = person,
                Roles = new List<Role>()
            };
        }

        /// <summary>
        /// Gets or sets the album credit identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id")]
        public Guid AlbumId { get; set; }

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
        /// Gets or sets the roles this person had on the album.
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
