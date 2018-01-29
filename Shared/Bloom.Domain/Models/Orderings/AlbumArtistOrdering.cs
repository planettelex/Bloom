using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumArtistOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// c7432618-7148-4e0d-8932-4fb7ba08ae12
        /// </value>
        public Guid Id => Guid.Parse("c7432618-7148-4e0d-8932-4fb7ba08ae12");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Artist
        /// </value>
        public string Label => "Album Artist";

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
