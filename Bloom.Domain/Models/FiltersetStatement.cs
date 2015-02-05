using System;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a filterset statement.
    /// </summary>
    public class FiltersetStatement
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the filter identifier.
        /// </summary>
        public Guid FilterId { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public IFiltersetFilter Filter { get; set; }

        /// <summary>
        /// Gets or sets the filter item scope.
        /// </summary>
        public FiltersetItemScope Scope { get; set; }

        /// <summary>
        /// Gets or sets the filter comparison.
        /// </summary>
        public FilterComparison Comparison { get; set; }
    }
}
