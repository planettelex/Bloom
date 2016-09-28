using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a set of filters and orders.
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Prism.Mvvm.BindableBase" />
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
        /// Gets or sets the elements of the filterset.
        /// </summary>
        public List<FiltersetElement> Elements { get; set; }

        /// <summary>
        /// Gets or sets the ordering of the filterset.
        /// </summary>
        public List<FiltersetOrder> Ordering { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return !string.IsNullOrEmpty(Name) ? Name : Id.ToString();
        }
    }
}
