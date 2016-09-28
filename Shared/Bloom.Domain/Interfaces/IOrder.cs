using System;

namespace Bloom.Domain.Interfaces
{
    /// <summary>
    /// Interface for an order.
    /// </summary>
    public interface IOrder
    {
        /// <summary>
        /// Gets the order identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the order label.
        /// </summary>
        string Label { get; }
    }
}
