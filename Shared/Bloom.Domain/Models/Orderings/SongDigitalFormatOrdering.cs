using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongDigitalFormatOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 7b08ad57-dc94-4827-9b34-cf68be0e3156
        /// </value>
        public Guid Id => Guid.Parse("7b08ad57-dc94-4827-9b34-cf68be0e3156");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Digital Format
        /// </value>
        public string Label => "Song Digital Format";

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
