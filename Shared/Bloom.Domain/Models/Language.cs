using System;
using System.Data.Linq.Mapping;
using Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a language.
    /// </summary>
    /// <seealso cref="BindableBase" />
    [Table(Name = "language")]
    public class Language : BindableBase
    {
        /// <summary>
        /// Creates a new language instance.
        /// </summary>
        /// <param name="name">The language name.</param>
        public static Language Create(string name)
        {
            return new Language
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        /// <summary>
        /// Gets or sets the language identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the language name.
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
