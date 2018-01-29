using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongKeyOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 05c3ca7c-099f-4764-b378-f426c0949bb1
        /// </value>
        public Guid Id => Guid.Parse("05c3ca7c-099f-4764-b378-f426c0949bb1");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Key
        /// </value>
        public string Label => "Song Key";

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
