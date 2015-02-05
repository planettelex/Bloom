using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a set of filterset ordering.
    /// </summary>
    public class FiltersetOrdering
    {
        /// <summary>
        /// Gets or sets the filterset ordering identifier.
        /// </summary>
        public Guid FiltersetId { get; set; }

        /// <summary>
        /// Gets or sets the filterset ordering priority.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the filterset order.
        /// </summary>
        public IFiltersetOrder Order { get; set; }

        /// <summary>
        /// Gets or sets the filterset item scope.
        /// </summary>
        public FiltersetItemScope Scope { get; set; }

        /// <summary>
        /// Gets or sets the order direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}
