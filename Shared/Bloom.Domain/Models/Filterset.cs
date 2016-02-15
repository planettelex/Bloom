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
        /// Creates a new filterset instance.
        /// </summary>
        /// <returns></returns>
        public static Filterset Create()
        {
            return new Filterset
            {
                Id = Guid.NewGuid()
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
        public string Name { get; set; }

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
