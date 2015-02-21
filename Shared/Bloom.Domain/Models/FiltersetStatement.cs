using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a filterset statement.
    /// </summary>
    [Table(Name = "filterset_statement")]
    public class FiltersetStatement
    {
        /// <summary>
        /// Creates a new filterset statement instance.
        /// </summary>
        /// <param name="filter">The statement filter.</param>
        /// <param name="comparison">The statement comparison.</param>
        /// <param name="scope">The scope of the statement.</param>
        public static FiltersetStatement Create(IFiltersetFilter filter, FilterComparison comparison, FiltersetItemScope scope)
        {
            return new FiltersetStatement
            {
                Id = Guid.NewGuid(),
                FilterId = filter.Id,
                Filter = filter,
                Comparison = comparison,
                Scope = scope
            };
        }

        /// <summary>
        /// Gets or sets the filterset statement identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the filter identifier.
        /// </summary>
        [Column(Name = "filter_id")]
        public Guid FilterId { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public IFiltersetFilter Filter { get; set; }

        /// <summary>
        /// Gets or sets the filter item scope.
        /// </summary>
        [Column(Name = "scope")]
        public FiltersetItemScope Scope { get; set; }

        /// <summary>
        /// Gets or sets the filter comparison.
        /// </summary>
        [Column(Name = "comparison")]
        public FilterComparison Comparison { get; set; }
    }
}
