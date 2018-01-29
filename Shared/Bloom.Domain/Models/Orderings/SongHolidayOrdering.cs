using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongHolidayOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 9394e4c6-cb4f-4863-8dc3-d3cc7ff0feee
        /// </value>
        public Guid Id => Guid.Parse("9394e4c6-cb4f-4863-8dc3-d3cc7ff0feee");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Holiday
        /// </value>
        public string Label => "Song Holiday";

        /// <summary>
        /// Returns a new collection of the specified type which has been filtered using a provided comparison.
        /// </summary>
        /// <typeparam name="T">Domain entity to filter.</typeparam>
        /// <param name="items">The collection to filter.</param>
        /// <param name="direction">The order direction.</param>
        public List<T> Apply<T>(List<T> items, OrderingDirection direction)
        {
            throw new NotImplementedException();
        }
    }
}
