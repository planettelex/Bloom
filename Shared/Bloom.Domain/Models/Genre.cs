using System;
using System.Data.Linq.Mapping;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a music genre.
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Prism.Mvvm.BindableBase" />
    [Table(Name = "genre")]
    public class Genre : BindableBase
    {
        /// <summary>
        /// Creates a new genre instance.
        /// </summary>
        /// <param name="name">The genre name.</param>
        public static Genre Create(string name)
        {
            return new Genre
            {
                Id = Guid.NewGuid(),
                Name = name,
            };
        }

        /// <summary>
        /// Creates a new genre instance.
        /// </summary>
        /// <param name="name">The genre name.</param>
        /// <param name="parentGenre">The parent genre.</param>
        public static Genre Create(string name, Genre parentGenre)
        {
            return new Genre
            {
                Id = Guid.NewGuid(),
                Name = name,
                ParentGenre = parentGenre
            };
        }

        /// <summary>
        /// Gets or sets the genre identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the genre name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;

        /// <summary>
        /// Gets or sets the genre description.
        /// </summary>
        [Column(Name = "description")]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        private string _description;

        /// <summary>
        /// Gets or sets the parent genre identifier.
        /// </summary>
        [Column(Name = "parent_genre_id")]
        public Guid? ParentGenreId { get; set; }

        /// <summary>
        /// Gets or sets the parent genre.
        /// </summary>
        public Genre ParentGenre 
        { 
            get { return _parentGenre; }
            set
            {
                _parentGenre = value;
                ParentGenreId = _parentGenre == null ? (Guid?) null : _parentGenre.Id;
            } 
        }
        private Genre _parentGenre;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}
