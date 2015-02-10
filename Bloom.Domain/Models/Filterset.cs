using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a set of filters and orders on a library.
    /// </summary>
    [Table(Name = "filterset")]
    public class Filterset
    {
        /// <summary>
        /// Gets or sets the filterset identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the filterset name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the elements of the filterset.
        /// </summary>
        public List<FiltersetElement> Elements { get; set; }

        /// <summary>
        /// Gets or sets the ordering of the filterset.
        /// </summary>
        public List<FiltersetOrdering> Ordering { get; set; } 
    }
}
