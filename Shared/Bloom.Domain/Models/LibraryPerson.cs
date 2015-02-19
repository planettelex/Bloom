using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a library with a person.
    /// </summary>
    [Table(Name = "library_person")]
    public class LibraryPerson
    {
        /// <summary>
        /// Creates a new library person instance.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="person">The person.</param>
        public static LibraryPerson Create(Library library, Person person)
        {
            return new LibraryPerson
            {
                LibraryId = library.Id,
                PersonId = person.Id,
                Person = person
            };
        }
        
        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        public Guid LibraryId { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        [Column(Name = "person_id", IsPrimaryKey = true)]
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this library follows this person.
        /// </summary>
        [Column(Name = "follow")]
        public bool Follow { get; set; }
    }
}
