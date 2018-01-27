using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumIsHolidayFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// edcf2147-c184-4ce6-a31c-e235702c389c
        /// </value>
        public Guid Id => Guid.Parse("edcf2147-c184-4ce6-a31c-e235702c389c");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Album Is Holiday
        /// </value>
        public string Label => "Album Is Holiday";

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
