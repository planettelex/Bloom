using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongVersionOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 73a7fe69-0aa7-449f-9e3c-e26c3265f19b
        /// </value>
        public Guid Id => Guid.Parse("73a7fe69-0aa7-449f-9e3c-e26c3265f19b");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Version
        /// </value>
        public string Label => "Song Version";

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
