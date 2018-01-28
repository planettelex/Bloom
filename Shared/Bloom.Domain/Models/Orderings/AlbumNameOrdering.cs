using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumNameOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// be5a0f6d-f0c0-455f-9967-af9c0f947bce
        /// </value>
        public Guid Id => Guid.Parse("be5a0f6d-f0c0-455f-9967-af9c0f947bce");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Name
        /// </value>
        public string Label => "Album Name";

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
