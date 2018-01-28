using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class SongGenreFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 3cdfacd0-be33-40bc-b814-eb46218a19e6
        /// </value>
        public Guid Id => Guid.Parse("3cdfacd0-be33-40bc-b814-eb46218a19e6");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Genre
        /// </value>
        public string Label => "Song Genre";

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
