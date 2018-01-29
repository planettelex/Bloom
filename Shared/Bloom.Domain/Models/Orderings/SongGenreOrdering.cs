using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongGenreOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 9c068032-00a3-4bb6-9eac-37dcaec86445
        /// </value>
        public Guid Id => Guid.Parse("9c068032-00a3-4bb6-9eac-37dcaec86445");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Genre
        /// </value>
        public string Label => "Song Genre";

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
