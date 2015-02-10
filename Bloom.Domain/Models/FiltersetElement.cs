using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a single element in a filterset.
    /// </summary>
    [Table(Name = "filterset_element")]
    public class FiltersetElement
    {
        /// <summary>
        /// Gets or sets the filterset identifier.
        /// </summary>
        [Column(Name = "filterset_id", IsPrimaryKey = true)]
        public Guid FiltersetId { get; set; }

        /// <summary>
        /// Gets or sets the filterset element order.
        /// </summary>
        [Column(Name = "order", IsPrimaryKey = true)]
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the filterset element type.
        /// </summary>
        [Column(Name = "element_type")]
        public FiltersetElementType ElementType { get; set; }

        /// <summary>
        /// Gets or sets the filterset element statement identifier.
        /// </summary>
        [Column(Name = "statement_id")]
        public Guid StatementId { get; set; }

        /// <summary>
        /// Gets or sets the filterset element statement.
        /// </summary>
        public FiltersetStatement Statement { get; set; }
    }
}
