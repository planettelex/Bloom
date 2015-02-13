using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a person with a reference.
    /// </summary>
    [Table(Name = "person_reference")]
    public class PersonReference
    {
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        [Column(Name = "person_id", IsPrimaryKey = true)]
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        [Column(Name = "reference_id", IsPrimaryKey = true)]
        public Guid ReferenceId { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        public Reference Reference { get; set; }
    }
}
