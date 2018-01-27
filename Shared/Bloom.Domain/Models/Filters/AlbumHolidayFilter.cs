using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumHolidayFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 4ef890db-5264-4886-a70c-358e8876d77f
        /// </value>
        public Guid Id => Guid.Parse("4ef890db-5264-4886-a70c-358e8876d77f");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Album Holiday
        /// </value>
        public string Label => "Album Holiday";

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
