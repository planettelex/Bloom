using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumIsBootlegFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// a89a755e-0dc5-4868-82ba-78e56c9d5739
        /// </value>
        public Guid Id => Guid.Parse("a89a755e-0dc5-4868-82ba-78e56c9d5739");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Album Is Bootleg
        /// </value>
        public string Label => "Album Is Bootleg";

        /// <summary>
        /// Returns a new collection of the specified type which has been filtered using a provided comparison.
        /// </summary>
        /// <typeparam name="T">Domain entity to filter.</typeparam>
        /// <param name="items">The collection to filter.</param>
        /// <param name="comparison">The comparison statement.</param>
        /// <param name="compareAgainst">The value to compare against.</param>
        public List<T> Apply<T>(List<T> items, FilterComparison comparison, string compareAgainst)
        {
            throw new NotImplementedException();
        }
    }
}
