using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumIsPromotionalFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 21ee4809-cec8-47c9-885b-b6d4105214cb
        /// </value>
        public Guid Id => Guid.Parse("21ee4809-cec8-47c9-885b-b6d4105214cb");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Album Is Promotional
        /// </value>
        public string Label => "Album Is Promotional";

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
