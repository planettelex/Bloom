using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumIsHolidayOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// 189d4bdb-a502-40f1-b2d0-47346327509b
        /// </value>
        public Guid Id => Guid.Parse("189d4bdb-a502-40f1-b2d0-47346327509b");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Is Holiday
        /// </value>
        public string Label => "Album Is Holiday";

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
