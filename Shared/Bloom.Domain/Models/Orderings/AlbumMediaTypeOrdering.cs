using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumMediaTypeOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// 7ce8dca3-69e4-4a9b-a6b4-4a670efacdfd
        /// </value>
        public Guid Id => Guid.Parse("7ce8dca3-69e4-4a9b-a6b4-4a670efacdfd");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Media
        /// </value>
        public string Label => "Album Media";

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
