using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a song with a person.
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
                PersonId = person.Id,
                Person = person
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
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets whether this person has a "featured" credit.
        /// </summary>
        [Column(Name = "is_featured")]
        public bool IsFeatured { get; set; }

        /// <summary>
        /// Gets or sets the person's roles on the song.
        /// </summary>
        public List<Role> Roles { get; set; }
    }
}
