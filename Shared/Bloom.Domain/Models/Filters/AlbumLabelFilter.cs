using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumLabelFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 8b5e0ccf-0c88-4fd4-903d-5f61d86ae1cd
        /// </value>
        public Guid Id => Guid.Parse("8b5e0ccf-0c88-4fd4-903d-5f61d86ae1cd");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Record Label
        /// </value>
        public string Label => "Record Label";

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
