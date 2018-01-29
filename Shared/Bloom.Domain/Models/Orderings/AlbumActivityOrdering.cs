using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumActivityOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// 79a2a4e2-5c88-4992-ac23-c06a24814376
        /// </value>
        public Guid Id => Guid.Parse("79a2a4e2-5c88-4992-ac23-c06a24814376");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Activity
        /// </value>
        public string Label => "Album Activity";

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
