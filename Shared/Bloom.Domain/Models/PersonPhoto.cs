using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a person with a photo.
    /// </summary>
    [Table(Name = "person_photo")]
    public class PersonPhoto
    {
        /// <summary>
        /// Creates a new person photo instance.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="photo">The photo.</param>
        /// <param name="priority">The order priority.</param>
        public static PersonPhoto Create(Person person, Photo photo, int priority)
        {
            return new PersonPhoto
            {
                PersonId = person.Id,
                PhotoId = photo.Id,
                Photo = photo,
                Priority = priority
            };
        }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        [Column(Name = "person_id", IsPrimaryKey = true)]
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the photo identifier.
        /// </summary>
        [Column(Name = "photo_id", IsPrimaryKey = true)]
        public Guid PhotoId { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public Photo Photo { get; set; }

        /// <summary>
        /// Gets or sets the photo order priority.
        /// </summary>
        [Column(Name = "priority")]
        public int Priority { get; set; }
    }
}
