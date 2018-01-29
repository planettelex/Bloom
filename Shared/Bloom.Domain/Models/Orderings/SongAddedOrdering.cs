using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongAddedOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 1c2bf482-7ca4-4818-88e6-0e805ffd8f44
        /// </value>
        public Guid Id => Guid.Parse("1c2bf482-7ca4-4818-88e6-0e805ffd8f44");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Added
        /// </value>
        public string Label => "Song Added";

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
