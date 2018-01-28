using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class SongIsLiveFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// a19c9999-3253-4240-8a46-c3639a1ec2b8
        /// </value>
        public Guid Id => Guid.Parse("a19c9999-3253-4240-8a46-c3639a1ec2b8");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Is Live
        /// </value>
        public string Label => "Song Is Live";

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
