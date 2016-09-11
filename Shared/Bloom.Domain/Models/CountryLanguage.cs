using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a country and a language.
    /// </summary>
    [Table(Name = "country_language")]
    public class CountryLanguage
    {
        /// <summary>
        /// Creates a new country language instance.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="language">The language.</param>
        public static CountryLanguage Create(Country country, Language language)
        {
            return new CountryLanguage
            {
                CountryId = country.Id,
                LanguageId = language.Id
            };
        }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        [Column(Name = "country_id", IsPrimaryKey = true)]
        public Guid CountryId { get; set; }

        /// <summary>
        /// Gets or sets the language identifier.
        /// </summary>
        [Column(Name = "language_id", IsPrimaryKey = true)]
        public Guid LanguageId { get; set; }
    }
}
