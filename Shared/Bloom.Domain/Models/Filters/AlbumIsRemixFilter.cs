using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumIsRemixFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 1add1df8-a9ae-45a9-9595-5a1a1524bf26
        /// </value>
        public Guid Id => Guid.Parse("1add1df8-a9ae-45a9-9595-5a1a1524bf26");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Album Is Remix
        /// </value>
        public string Label => "Album Is Remix";

        /// <summary>
        /// Returns a new collection of the specified type which has been filtered using a provided comparison.
        /// </summary>
        /// <typeparam name="T">Domain entity to filter.</typeparam>
        /// <param name="items">The collection to filter.</param>
        /// <param name="comparison">The comparison statement.</param>
        /// <param name="compareAgainst">The value to compare against.</param>
        public List<T> Apply<T>(List<T> items, FilterComparison comparison, string compareAgainst)
        {
            throw new NotImplementedException();
        }
    }
}
