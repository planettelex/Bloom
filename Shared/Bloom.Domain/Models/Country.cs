using System;
using System.Data.Linq.Mapping;
using Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a country.
    /// </summary>
    /// <seealso cref="BindableBase" />
    [Table(Name = "country")]
    public class Country : BindableBase
    {
        /// <summary>
        /// Creates a new country instance.
        /// </summary>
        /// <param name="name">The country name.</param>
        public static Country Create(string name)
        {
            return new Country
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the country name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}
