using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class SongIsRemixFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 0a8f06c1-8bbc-4ef0-adba-87b3b4d21ff3
        /// </value>
        public Guid Id => Guid.Parse("0a8f06c1-8bbc-4ef0-adba-87b3b4d21ff3");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Is Remix
        /// </value>
        public string Label => "Song Is Remix";

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
