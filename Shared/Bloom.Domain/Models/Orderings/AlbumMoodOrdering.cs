using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumMoodOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// e0c5f400-8b6f-4f2e-ab87-b18325d4b890
        /// </value>
        public Guid Id => Guid.Parse("e0c5f400-8b6f-4f2e-ab87-b18325d4b890");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Mood
        /// </value>
        public string Label => "Album Mood";

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
