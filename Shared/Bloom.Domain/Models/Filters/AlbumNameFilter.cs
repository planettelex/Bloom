using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Filters
{
    public class AlbumNameFilter : IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        /// <value>
        /// 59415c93-8032-4478-af70-b619f6a18c20
        /// </value>
        public Guid Id => Guid.Parse("59415c93-8032-4478-af70-b619f6a18c20");

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        /// <value>
        /// Album Name Filter
        /// </value>
        public string Label => "Album Name Filter";

        /// <summary>
        /// Applies this filter to the provided songs.
        /// </summary>
        /// <param name="songs">The songs.</param>
        /// <param name="comparison">The filter comparison.</param>
        public List<Song> Apply(List<Song> songs, FilterComparison comparison)
        {
            throw new NotImplementedException(); //TODO: Add apply methods to interface.
        }
    }
}
