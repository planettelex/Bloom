using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumIsPromotionalOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// 959a89dd-2894-4589-92d5-85654f10bfd0
        /// </value>
        public Guid Id => Guid.Parse("959a89dd-2894-4589-92d5-85654f10bfd0");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Is Promotional
        /// </value>
        public string Label => "Album Is Promotional";

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
