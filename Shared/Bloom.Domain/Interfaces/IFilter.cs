using System;

namespace Bloom.Domain.Interfaces
{
    /// <summary>
    /// Interface for a filter.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Gets the filter identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the filter label.
        /// </summary>
        string Label { get; }
    }
}
