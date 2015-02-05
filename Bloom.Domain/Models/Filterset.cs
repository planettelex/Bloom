using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a set of filter and orders on a library.
    /// </summary>
    public class Filterset
    {
        /// <summary>
        /// Gets or sets the filterset identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the filterset name.
        /// </summary>
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
