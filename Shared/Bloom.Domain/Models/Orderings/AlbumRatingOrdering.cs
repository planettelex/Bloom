using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumRatingOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// 759c9861-2306-4006-888a-88e88e67883b
        /// </value>
        public Guid Id => Guid.Parse("759c9861-2306-4006-888a-88e88e67883b");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Rating
        /// </value>
        public string Label => "Album Rating";

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
