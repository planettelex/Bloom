using System;
using System.Data.Linq.Mapping;
using Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a city.
    /// </summary>
    /// <seealso cref="BindableBase" />
    [Table(Name = "city")]
    public class City : BindableBase
    {
        /// <summary>
        /// Creates a new city instance.
        /// </summary>
        /// <param name="name">The city name.</param>
        public static City Create(string name)
        {
            return new City
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the city identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;

        /// <summary>
        /// Gets or sets the country region identifier.
        /// </summary>
        [Column(Name = "country_region_id")]
        public Guid? CountryRegionId { get; set; }

        /// <summary>
        /// Gets or sets the country region.
        /// </summary>
        public CountryRegion CountryRegion
        {
            get { return _countryRegion; }
            set
            {
                _countryRegion = value;
                CountryRegionId = _countryRegion?.Id;
            }
        }
        private CountryRegion _countryRegion;

        /// <summary>
        /// Gets or sets the country identifier. Use this property only when there is no country region.
        /// </summary>
        [Column(Name = "country_id")]
        public Guid CountryId { get; set; }

        /// <summary>
        /// Gets or sets the country. Use this property only when there is no country region.
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
        /// Gets or sets the city's latitude.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the city's longitude.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}
