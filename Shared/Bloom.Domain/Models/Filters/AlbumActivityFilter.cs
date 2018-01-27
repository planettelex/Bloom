using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumActivityFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 2ee67c5d-aa9a-4e0e-a185-f4a9a4b1bbae
        /// </value>
        public Guid Id => Guid.Parse("2ee67c5d-aa9a-4e0e-a185-f4a9a4b1bbae");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Album Activity
        /// </value>
        public string Label => "Album Activity";

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
