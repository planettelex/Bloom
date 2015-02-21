using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumNameFilter : IFiltersetFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 59415c93-8032-4478-af70-b619f6a18c20
        /// </value>
        public Guid Id
        {
            get { return Guid.Parse("59415c93-8032-4478-af70-b619f6a18c20"); }
        }

        /// <summary>
        /// Gets the filter name.
        /// </summary>
        /// <value>
        /// AlbumNameFilter
        /// </value>
        public string Name { get { return "AlbumNameFilter"; } }

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Album Name Filter
        /// </value>
        public string Label { get { return "Album Name Filter"; } }

        /// <summary>
        /// Gets the followup filter identifier.
        /// </summary>
        /// <value>
        /// 00000000-0000-0000-0000-000000000000
        /// </value>
        public Guid FollowupFilterId { get { return Guid.Empty; } }

        /// <summary>
        /// Gets the followup filter.
        /// </summary>
        public IFiltersetFilter FollowupFilter { get { return null; } }

        /// <summary>
        /// Applies this filter to the provided songs.
        /// </summary>
        /// <param name="scope">The filter scope.</param>
        /// <param name="songs">The songs.</param>
        /// <param name="comparison">The filter comparison.</param>
        public List<Song> Apply(FiltersetItemScope scope, List<Song> songs, FilterComparison comparison)
        {
            throw new NotImplementedException();
        }
    }
}
