using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models.Orders
{
    public class AlbumNameOrder : IOrder
    {
        /// <summary>
        /// Gets the order identifier.
        /// </summary>
        /// <value>
        /// be5a0f6d-f0c0-455f-9967-af9c0f947bce
        /// </value>
        public Guid Id { get { return Guid.Parse("be5a0f6d-f0c0-455f-9967-af9c0f947bce"); } }

        /// <summary>
        /// Gets the order label.
        /// </summary>
        /// <value>
        /// Album Name Order
        /// </value>
        public string Label { get { return "Album Name Order"; } }

        /// <summary>
        /// Applies this order to the provided songs.
        /// </summary>
        /// <param name="songs">The songs.</param>
        /// <param name="direction">The order direction.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Apply(ref List<Song> songs, OrderDirection direction)
        {
            throw new NotImplementedException(); //TODO: Add apply methods to interface.
        }
    }
}
