﻿ using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with a person.
    /// </summary>
    [Table(Name = "album_credit")]
    public class AlbumCredit
    {
        /// <summary>
        /// Creates a new album credit instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="person">The person credited.</param>
        public static AlbumCredit Create(Album album, Person person)
        {
            return new AlbumCredit
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                PersonId = person.Id,
                Person = person
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
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets the roles this person had on the album.
        /// </summary>
        public List<Role> Roles { get; set; }

    }
}
