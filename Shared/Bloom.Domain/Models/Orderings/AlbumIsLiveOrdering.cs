using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumIsLiveOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// 4208d0f4-447e-418f-be11-5de5f1b86683
        /// </value>
        public Guid Id => Guid.Parse("4208d0f4-447e-418f-be11-5de5f1b86683");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Is Live
        /// </value>
        public string Label => "Album Is Live";

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
