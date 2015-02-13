using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a label with a person.
    /// </summary>
    [Table(Name = "label_personel")]
    public class LabelPersonel
    {
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
        /// Gets or sets the person.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets the personel roles.
        /// </summary>
        public List<LabelPersonelRole> Roles { get; set; } 
    }
}
