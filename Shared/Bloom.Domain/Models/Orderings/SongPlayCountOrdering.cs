using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongPlayCountOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 2239203d-3019-4590-a2ae-3e851ba1c9c4
        /// </value>
        public Guid Id => Guid.Parse("2239203d-3019-4590-a2ae-3e851ba1c9c4");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Play Count
        /// </value>
        public string Label => "Song Play Count";

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
