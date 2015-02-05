using System;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a set of filterset element.
    /// </summary>
    public class FiltersetElement
    {
        /// <summary>
        /// Gets or sets the filterset identifier.
        /// </summary>
        public Guid FiltersetId { get; set; }

        /// <summary>
        /// Gets or sets the filterset element order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the filterset element type.
        /// </summary>
        public FiltersetElementType ElementType { get; set; }

        /// <summary>
        /// Gets or sets the filterset element statement identifier.
        /// </summary>
        public Guid StatementId { get; set; }

        /// <summary>
        /// Gets or sets the filterset element statement.
        /// </summary>
        public FiltersetStatement Statement { get; set; }
    }
}
