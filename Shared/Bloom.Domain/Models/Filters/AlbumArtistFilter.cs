using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumArtistFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 3d91deb2-664e-41da-947e-4e1cdc58a4f1
        /// </value>
        public Guid Id => Guid.Parse("3d91deb2-664e-41da-947e-4e1cdc58a4f1");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Album Artist
        /// </value>
        public string Label => "Album Artist";

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
