using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumTagOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// c157327a-0375-439e-afe4-685b80ab1516
        /// </value>
        public Guid Id => Guid.Parse("c157327a-0375-439e-afe4-685b80ab1516");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Tag
        /// </value>
        public string Label => "Album Tag";

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
