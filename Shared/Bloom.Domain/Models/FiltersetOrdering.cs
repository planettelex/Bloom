using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a set of filterset ordering.
    /// </summary>
    [Table(Name = "filterset_ordering")]
    public class FiltersetOrdering
    {
        /// <summary>
        /// Gets or sets the filterset ordering identifier.
        /// </summary>
        [Column(Name = "filterset_id", IsPrimaryKey = true)]
        public Guid FiltersetId { get; set; }

        /// <summary>
        /// Gets or sets the filterset ordering priority.
        /// </summary>
        [Column(Name = "priority", IsPrimaryKey = true)]
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        [Column(Name = "order_id")]
        public Guid OrderId { get; set; }

        /// <summary>
        /// Gets or sets the filterset order.
        /// </summary>
        public IFiltersetOrder Order { get; set; }

        /// <summary>
        /// Gets or sets the filterset item scope.
        /// </summary>
        [Column(Name = "scope")]
        public FiltersetItemScope Scope { get; set; }

        /// <summary>
        /// Gets or sets the order direction.
        /// </summary>
        [Column(Name = "direction")]
        public OrderDirection Direction { get; set; }
    }
}
