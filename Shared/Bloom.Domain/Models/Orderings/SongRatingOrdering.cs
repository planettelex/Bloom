using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongRatingOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 82d0ce85-1ab2-4194-bf46-6d6c73766bd0
        /// </value>
        public Guid Id => Guid.Parse("82d0ce85-1ab2-4194-bf46-6d6c73766bd0");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Rating
        /// </value>
        public string Label => "Song Rating";

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
