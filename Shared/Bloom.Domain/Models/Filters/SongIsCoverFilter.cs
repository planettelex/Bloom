using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class SongIsCoverFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// ee933c28-1c76-4978-846c-dab48ebac6d9
        /// </value>
        public Guid Id => Guid.Parse("ee933c28-1c76-4978-846c-dab48ebac6d9");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Is Cover
        /// </value>
        public string Label => "Song Is Cover";

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
