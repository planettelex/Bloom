using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orderings
{
    public class SongIsCoverOrdering : IOrdering
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 9418a0ac-936e-4055-bbb3-a51c80d10583
        /// </value>
        public Guid Id => Guid.Parse("9418a0ac-936e-4055-bbb3-a51c80d10583");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Is Cover
        /// </value>
        public string Label => "Song Is Cover";

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
