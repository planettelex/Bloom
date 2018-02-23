using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a set of filters and orders.
    /// </summary>
    /// <seealso cref="BindableBase" />
    [Table(Name = "filterset")]
    public class Filterset : BindableBase
    {
        /// <summary>
        /// Creates a new filterset instance.
        /// </summary>
        public static Filterset Create()
        {
            return new Filterset
            {
                Id = Guid.NewGuid(),
                AlbumFilterExpression = new List<FiltersetExpressionElement>(),
                AlbumOrdering = new List<FiltersetOrderingElement>(),
                CreatedOn = DateTime.Now
            };
        }

        /// <summary>
        /// Creates a new filterset instance.
        /// </summary>
        public static Filterset Create(string name)
        {
            return new Filterset
            {
                Id = Guid.NewGuid(),
                Name = name,
                AlbumFilterExpression = new List<FiltersetExpressionElement>(),
                AlbumOrdering = new List<FiltersetOrderingElement>(),
                CreatedOn = DateTime.Now
            };
        }
        
        /// <summary>
        /// Gets or sets the filterset identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the filterset name.
        /// </summary>
        [Column(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _name;

        /// <summary>
        /// Gets or sets the date this filterset was created on.
        /// </summary>
        [Column(Name = "created_on")]
        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { SetProperty(ref _createdOn, value); }
        }
        private DateTime _createdOn;

        /// <summary>
        /// Gets or sets the elements of the album filterset expression.
        /// </summary>
        public List<FiltersetExpressionElement> AlbumFilterExpression { get; set; }

        /// <summary>
        /// Gets or sets the elements of the song filterset expression.
        /// </summary>
        public List<FiltersetExpressionElement> SongFilterExpression { get; set; }

        /// <summary>
        /// Gets or sets the album ordering of the filterset.
        /// </summary>
        public List<FiltersetOrderingElement> AlbumOrdering { get; set; }

        /// <summary>
        /// Gets or sets the song ordering of the filterset.
        /// </summary>
        public List<FiltersetOrderingElement> SongOrdering { get; set; }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return !string.IsNullOrEmpty(Name) ? Name : Id.ToString();
        }
    }
}
