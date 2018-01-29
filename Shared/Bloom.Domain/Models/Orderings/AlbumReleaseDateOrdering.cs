using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumReleaseDateOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// ec97c19d-cc08-4d40-a9e3-b98b0ffdc4c7
        /// </value>
        public Guid Id => Guid.Parse("ec97c19d-cc08-4d40-a9e3-b98b0ffdc4c7");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Release Date
        /// </value>
        public string Label => "Album Release Date";

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
