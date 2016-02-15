using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a single element in a filterset.
    /// </summary>
    [Table(Name = "filterset_element")]
    public class FiltersetElement
    {
        /// <summary>
        /// Creates a new filterset element instance.
        /// </summary>
        /// <param name="filterset">The filterset.</param>
        /// <param name="elementType">The type of element.</param>
        /// <param name="elementNumber">The element number.</param>
        public static FiltersetElement Create(Filterset filterset, FiltersetElementType elementType, int elementNumber)
        {
            return new FiltersetElement
            {
                FiltersetId = filterset.Id,
                ElementType = elementType,
                ElementNumber = elementNumber
            };
        }

        /// <summary>
        /// Creates a new filterset element instance.
        /// </summary>
        /// <param name="filterset">The filterset.</param>
        /// <param name="elementNumber">The element number.</param>
        public static FiltersetElement Create(Filterset filterset, int elementNumber)
        {
            return new FiltersetElement
            {
                FiltersetId = filterset.Id,
                ElementType = FiltersetElementType.Statement,
                ElementNumber = elementNumber
            };
        }

        /// <summary>
        /// Gets or sets the filterset identifier.
        /// </summary>
        [Column(Name = "filterset_id", IsPrimaryKey = true)]
        public Guid FiltersetId { get; set; }

        /// <summary>
        /// Gets or sets the filterset element number.
        /// </summary>
        [Column(Name = "element_number", IsPrimaryKey = true)]
        public int ElementNumber { get; set; }

        /// <summary>
        /// Gets or sets the filterset element type.
        /// </summary>
        [Column(Name = "element_type")]
        public FiltersetElementType ElementType { get; set; }

        /// <summary>
        /// Gets or sets the filter identifier.
        /// </summary>
        [Column(Name = "filter_id")]
        public Guid FilterId { get; set; }

        /// <summary>
        /// Gets or sets the filter comparison.
        /// </summary>
        [Column(Name = "filter_comparison")]
        public FilterComparison Comparison { get; set; }

        /// <summary>
        /// Gets or sets the value to filter against.
        /// </summary>
        [Column(Name = "filter_against")]
        public string FilterAgainst { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public IFilter Filter { get; set; }
    }
}
