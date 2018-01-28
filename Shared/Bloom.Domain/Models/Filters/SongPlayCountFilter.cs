using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class SongPlayCountFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// d9cb7209-bdad-4b98-8884-b3f6697cf184
        /// </value>
        public Guid Id => Guid.Parse("d9cb7209-bdad-4b98-8884-b3f6697cf184");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Play Count
        /// </value>
        public string Label => "Song Play Count";

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
