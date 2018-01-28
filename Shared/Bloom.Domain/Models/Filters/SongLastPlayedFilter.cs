using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class SongLastPlayedFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// aab06bda-e4f6-48ce-8a71-298611a8d582
        /// </value>
        public Guid Id => Guid.Parse("aab06bda-e4f6-48ce-8a71-298611a8d582");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Last Played
        /// </value>
        public string Label => "Song Last Played";

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
