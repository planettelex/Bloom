using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumLenghthTypeOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// c5e79e25-547a-423c-87f8-f7f4f3e2655b
        /// </value>
        public Guid Id => Guid.Parse("c5e79e25-547a-423c-87f8-f7f4f3e2655b");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Type
        /// </value>
        public string Label => "Album Type";

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
