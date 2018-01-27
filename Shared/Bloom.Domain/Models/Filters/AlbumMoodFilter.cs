using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumMoodFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 70b7c964-3296-4e39-a0ed-8ce41c6f3b1a
        /// </value>
        public Guid Id => Guid.Parse("70b7c964-3296-4e39-a0ed-8ce41c6f3b1a");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Album Mood
        /// </value>
        public string Label => "Album Mood";

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
