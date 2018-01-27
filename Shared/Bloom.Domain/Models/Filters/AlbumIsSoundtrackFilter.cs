using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumIsSoundtrackFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// c7f44a8a-abfa-486b-b6a1-0a54596a413c
        /// </value>
        public Guid Id => Guid.Parse("c7f44a8a-abfa-486b-b6a1-0a54596a413c");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Album Is Soundtrack
        /// </value>
        public string Label => "Album Is Soundtrack";

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
