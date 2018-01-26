using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a role a person may have.
    /// </summary>
    [Table(Name = "role")]
    public class Role
    {
        /// <summary>
        /// Creates a new role instance.
        /// </summary>
        /// <param name="name">The role name.</param>
        public static Role Create(string name)
        {
            return new Role
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the role name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Predefined composer role.
        /// </summary>
        public static Role Composer => new Role
        {
            Id = Guid.Parse("b243cf18-64a1-4755-9a2c-77e5db89615e"),
            Name = "Composer"
        };
    }
}
