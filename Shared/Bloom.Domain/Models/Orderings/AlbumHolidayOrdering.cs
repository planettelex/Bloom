using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumHolidayOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// 0b6611f8-5495-4561-a522-32d7b1207483
        /// </value>
        public Guid Id => Guid.Parse("0b6611f8-5495-4561-a522-32d7b1207483");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Holiday
        /// </value>
        public string Label => "Album Holiday";

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
