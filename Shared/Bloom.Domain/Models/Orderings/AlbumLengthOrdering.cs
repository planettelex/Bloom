using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class AlbumLengthOrdering : IOrdering
    {
        /// <summary>
        /// Gets the ordering identifier.
        /// </summary>
        /// <value>
        /// 54143f92-43ba-43ab-b482-fef01f81c9cc
        /// </value>
        public Guid Id => Guid.Parse("54143f92-43ba-43ab-b482-fef01f81c9cc");

        /// <summary>
        /// Gets the ordering label.
        /// </summary>
        /// <value>
        /// Album Length
        /// </value>
        public string Label => "Album Length";

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
