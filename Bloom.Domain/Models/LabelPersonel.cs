using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a label with a person.
    /// </summary>
    public class LabelPersonel
    {
        /// <summary>
        /// Gets or sets the label personel identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the label identifier.
        /// </summary>
        public Guid LabelId { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
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
