using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongMoodOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 31db4cdc-7373-4dff-9147-8a79b9b46239
        /// </value>
        public Guid Id => Guid.Parse("31db4cdc-7373-4dff-9147-8a79b9b46239");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Mood
        /// </value>
        public string Label => "Song Mood";

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
