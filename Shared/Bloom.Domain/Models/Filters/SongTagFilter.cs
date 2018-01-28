using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class SongTagFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// ab77e42d-c2ae-47e1-8e57-4bc323171b04
        /// </value>
        public Guid Id => Guid.Parse("ab77e42d-c2ae-47e1-8e57-4bc323171b04");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Tag
        /// </value>
        public string Label => "Song Tag";

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
