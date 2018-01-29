using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumLabelOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// 4fd6ef03-1f3f-4ceb-a631-904ca4b129d9
        /// </value>
        public Guid Id => Guid.Parse("4fd6ef03-1f3f-4ceb-a631-904ca4b129d9");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Record Label
        /// </value>
        public string Label => "Record Label";

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
