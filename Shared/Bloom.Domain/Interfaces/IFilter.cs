using System;

namespace Bloom.Domain.Interfaces
{
    public interface IFilter
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
    }
}
