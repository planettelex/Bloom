using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongArtistOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// ce662fd3-879d-4bdd-84e4-fe7d0d1e3534
        /// </value>
        public Guid Id => Guid.Parse("ce662fd3-879d-4bdd-84e4-fe7d0d1e3534");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Artist
        /// </value>
        public string Label => "Song Artist";

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
