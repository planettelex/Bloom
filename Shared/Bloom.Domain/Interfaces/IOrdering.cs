using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Interfaces
{
    /// <summary>
    /// Interface for an order.
    /// </summary>
    public interface IOrdering
    {
        /// <summary>
        /// Gets the order identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the order label.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Returns a new collection of the specified type which has been filtered using a provided comparison.
        /// </summary>
        /// <typeparam name="T">Domain entity to filter.</typeparam>
        /// <param name="items">The collection to filter.</param>
        /// <param name="direction">The order direction.</param>
        List<T> Apply<T>(List<T> items, OrderingDirection direction);
    }
}
