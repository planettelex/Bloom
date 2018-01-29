using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongNameOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 7a9f6dc0-f0ba-40a9-8526-704d1f901fcb
        /// </value>
        public Guid Id => Guid.Parse("7a9f6dc0-f0ba-40a9-8526-704d1f901fcb");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Name
        /// </value>
        public string Label => "Song Name";

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
