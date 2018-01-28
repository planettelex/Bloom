using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class SongIsHolidayFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 24053889-d5ab-4c4d-9988-8efd7d99e862
        /// </value>
        public Guid Id => Guid.Parse("24053889-d5ab-4c4d-9988-8efd7d99e862");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Is Holiday
        /// </value>
        public string Label => "Song Is Holiday";

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
