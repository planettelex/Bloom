using System;
using System.Data.Linq.Mapping;
using Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a country region, like a state or province.
    /// </summary>
    /// <seealso cref="BindableBase" />
    [Table(Name = "country_region")]
    public class CountryRegion : BindableBase
    {
        /// <summary>
        /// Creates a new country region instance.
        /// </summary>
        /// <param name="name">The country region name.</param>
        public static CountryRegion Create(string name)
        {
            return new CountryRegion
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the country region identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the country region name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        [Column(Name = "country_id")]
        public Guid CountryId { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public Country Country
        {
            get { return _country; }
            set
            {
                _country = value;
                CountryId = _country?.Id ?? Guid.Empty;
            }
        }
        private Country _country;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}
