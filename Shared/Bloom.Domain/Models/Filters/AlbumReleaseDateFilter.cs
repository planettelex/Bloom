using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumReleaseDateFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 2f7a07c0-f514-4658-bfa7-baecca28ce02
        /// </value>
        public Guid Id => Guid.Parse("2f7a07c0-f514-4658-bfa7-baecca28ce02");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Album Release Date
        /// </value>
        public string Label => "Album Release Date";

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
