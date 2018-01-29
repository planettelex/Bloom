using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongTimeSignatureOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 005e4b87-661e-4012-afe0-10139e6582f6
        /// </value>
        public Guid Id => Guid.Parse("005e4b87-661e-4012-afe0-10139e6582f6");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Time Signature
        /// </value>
        public string Label => "Song Time Signature";

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
