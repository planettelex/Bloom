using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumIsRemixOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// d419123c-fd9b-40d3-84e5-ddb98f7dfa75
        /// </value>
        public Guid Id => Guid.Parse("d419123c-fd9b-40d3-84e5-ddb98f7dfa75");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Is Remix
        /// </value>
        public string Label => "Album Is Remix";

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
