using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a set of filters and orders.
    /// </summary>
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
                Id = Guid.NewGuid()
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
                Name = name
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
        /// Gets or sets the elements of the filterset.
        /// </summary>
        public List<FiltersetElement> Elements { get; set; }

        /// <summary>
        /// Gets or sets the ordering of the filterset.
        /// </summary>
        public List<FiltersetOrder> Ordering { get; set; }

    }
}
