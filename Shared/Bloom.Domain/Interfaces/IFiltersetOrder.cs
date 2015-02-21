using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;

namespace Bloom.Domain.Interfaces
{
    public interface IFiltersetOrder
    {
        /// <summary>
        /// Gets the order identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the order name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the order label.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Applies this order to the provided songs.
        /// </summary>
        /// <param name="scope">The order scope.</param>
        /// <param name="songs">The songs.</param>
        /// <param name="direction">The order direction.</param>
        void Apply(FiltersetItemScope scope, ref List<Song> songs, OrderDirection direction);
    }
}
