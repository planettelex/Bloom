using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class SongCollaboratorFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// c4358657-be9f-4651-8273-0d899618f867
        /// </value>
        public Guid Id => Guid.Parse("c4358657-be9f-4651-8273-0d899618f867");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Song Collaborator
        /// </value>
        public string Label => "Song Collaborator";

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
