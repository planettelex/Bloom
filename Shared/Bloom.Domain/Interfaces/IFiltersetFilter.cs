using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;

namespace Bloom.Domain.Interfaces
{
    public interface IFiltersetFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the filter name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Gets the followup filter identifier.
        /// </summary>
        Guid FollowupFilterId { get; }

        /// <summary>
        /// Gets the followup filter.
        /// </summary>
        IFiltersetFilter FollowupFilter { get; }

        /// <summary>
        /// Applies this filter to the provided songs.
        /// </summary>
        /// <param name="scope">The filter scope.</param>
        /// <param name="songs">The songs.</param>
        /// <param name="comparison">The filter comparison.</param>
        List<Song> Apply(FiltersetItemScope scope, List<Song> songs, FilterComparison comparison);
    }
}
