using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongTagOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 2a7bb3d6-6ef9-41bb-8aef-134f86032373
        /// </value>
        public Guid Id => Guid.Parse("2a7bb3d6-6ef9-41bb-8aef-134f86032373");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Tag
        /// </value>
        public string Label => "Song Tag";

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
