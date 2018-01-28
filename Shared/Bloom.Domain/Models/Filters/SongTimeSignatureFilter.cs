using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class SongTimeSignatureFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 23436dde-c226-480d-b750-25bc7038dbed
        /// </value>
        public Guid Id => Guid.Parse("23436dde-c226-480d-b750-25bc7038dbed");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Time Signature
        /// </value>
        public string Label => "Song Time Signature";

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
