using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongIsExplicitOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// d3bfccf9-fb5d-4c00-bae7-a801503d170f
        /// </value>
        public Guid Id => Guid.Parse("d3bfccf9-fb5d-4c00-bae7-a801503d170f");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Has Explicit Content
        /// </value>
        public string Label => "Song Has Explicit Content";

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
