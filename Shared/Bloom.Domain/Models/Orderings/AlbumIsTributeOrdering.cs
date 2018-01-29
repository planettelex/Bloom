using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumIsTributeOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// 2171f326-95b9-4a4d-918b-392cf96f7aad
        /// </value>
        public Guid Id => Guid.Parse("2171f326-95b9-4a4d-918b-392cf96f7aad");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Is Tribute
        /// </value>
        public string Label => "Album Is Tribute";

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
