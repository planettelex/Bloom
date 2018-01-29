using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongBpmOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// afa86cfc-2250-49a2-8b30-06fc1f6e27e0
        /// </value>
        public Guid Id => Guid.Parse("afa86cfc-2250-49a2-8b30-06fc1f6e27e0");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song BPM
        /// </value>
        public string Label => "Song BPM";

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
