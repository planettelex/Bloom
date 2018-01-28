using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class SongHolidayFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// ed41f540-1991-4e75-a94a-536eff25f3aa
        /// </value>
        public Guid Id => Guid.Parse("ed41f540-1991-4e75-a94a-536eff25f3aa");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Holiday
        /// </value>
        public string Label => "Song Holiday";

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
