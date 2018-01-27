using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumMediaTypeFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// a7d6e4e4-ec7d-412a-ba86-76b03735672f
        /// </value>
        public Guid Id => Guid.Parse("a7d6e4e4-ec7d-412a-ba86-76b03735672f");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Album Media
        /// </value>
        public string Label => "Album Media";

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
