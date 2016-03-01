using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for source data.
    /// </summary>
    public interface ISourceRepository
    {
        /// <summary>
        /// Gets a source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="sourceId">The source identifier.</param>
        Source GetSource(IDataSource dataSource, Guid sourceId);

        /// <summary>
        /// Lists the sources.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Source> ListSources(IDataSource dataSource);

        /// <summary>
        /// Adds a source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="source">The source.</param>
        void AddSource(IDataSource dataSource, Source source);

        /// <summary>
        /// Deletes a source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="source">The source.</param>
        void DeleteSource(IDataSource dataSource, Source source);
    }
}
