using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a library with a person.
    /// </summary>
    [Table(Name = "library_person")]
    public class LibraryPerson
    {
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
