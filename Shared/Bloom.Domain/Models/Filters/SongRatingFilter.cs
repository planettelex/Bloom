using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class SongRatingFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 0deec55a-ef8b-4ab9-b3c0-0c86a6ad121f
        /// </value>
        public Guid Id => Guid.Parse("0deec55a-ef8b-4ab9-b3c0-0c86a6ad121f");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Rating
        /// </value>
        public string Label => "Song Rating";

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
