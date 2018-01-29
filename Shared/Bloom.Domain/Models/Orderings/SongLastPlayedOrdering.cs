using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongLastPlayedOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// b607c13d-2887-44fe-9525-47cd638ed5e8
        /// </value>
        public Guid Id => Guid.Parse("b607c13d-2887-44fe-9525-47cd638ed5e8");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Last Played
        /// </value>
        public string Label => "Song Last Played";

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
