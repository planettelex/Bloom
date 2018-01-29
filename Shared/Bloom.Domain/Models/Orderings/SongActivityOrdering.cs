using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongActivityOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 9ab7dcf3-480b-46a1-9bc0-e9e88e5031a7
        /// </value>
        public Guid Id => Guid.Parse("9ab7dcf3-480b-46a1-9bc0-e9e88e5031a7");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Activity
        /// </value>
        public string Label => "Song Activity";

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
