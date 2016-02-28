using System;

namespace Bloom.Domain.Interfaces
{
    /// <summary>
    /// A data order.
    /// </summary>
    public interface IOrder
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
    }
}
