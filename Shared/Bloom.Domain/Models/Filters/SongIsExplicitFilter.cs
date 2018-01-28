using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class SongIsExplicitFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 8529f68a-7afe-4df5-b738-10da72d3bb1a
        /// </value>
        public Guid Id => Guid.Parse("8529f68a-7afe-4df5-b738-10da72d3bb1a");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Has Explicit Content
        /// </value>
        public string Label => "Song Has Explicit Content";

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
