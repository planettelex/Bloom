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
        /// Creates a new label personel instance.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="person">The person.</param>
        public static LabelPersonel Create(Label label, Person person)
        {
            return new LabelPersonel
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
        /// Gets or sets the person.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets the personel roles.
        /// </summary>
        public List<LabelPersonelRole> Roles { get; set; }

        /// <summary>
        /// Creates and adds a role to this label personel.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>A new label personel role.</returns>
        /// <exception cref="System.ArgumentNullException">role</exception>
        public LabelPersonelRole AddRole(Role role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            if (Roles == null)
                Roles = new List<LabelPersonelRole>();

            var labelPersonelRole = LabelPersonelRole.Create(this, role);
            Roles.Add(labelPersonelRole);

            return labelPersonelRole;
        }
    }
}
