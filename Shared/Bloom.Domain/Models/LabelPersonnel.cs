using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a person who worked for a label.
    /// </summary>
    [Table(Name = "label_personnel")]
    public class LabelPersonnel
    {
        /// <summary>
        /// Creates a new label personel instance.
        /// </summary>
        /// <param name="label">A label.</param>
        /// <param name="person">The person.</param>
        public static LabelPersonnel Create(Label label, Person person)
        {
            return new LabelPersonnel
            {
                Id = Guid.NewGuid(),
                LabelId = label.Id,
                PersonId = person.Id,
                Person = person
            };
        }

        /// <summary>
        /// Gets or sets the label personel identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the label identifier.
        /// </summary>
        [Column(Name = "label_id")]
        public Guid LabelId { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        [Column(Name = "person_id")]
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the date this person started with this label.
        /// </summary>
        [Column(Name = "started")]
        public DateTime? Started { get; set; }

        /// <summary>
        /// Gets or sets the date this person ended with this label.
        /// </summary>
        [Column(Name = "ended")]
        public DateTime? Ended { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        [Column(Name = "priority")]
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets the personnel roles.
        /// </summary>
        public List<Role> Roles { get; set; }
    }
}
