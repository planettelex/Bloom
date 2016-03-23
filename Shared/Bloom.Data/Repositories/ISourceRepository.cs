using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for publication source data.
    /// </summary>
    public interface ISourceRepository
    {
        /// <summary>
        /// Determines whether a publication source exists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="sourceId">The publication source identifier.</param>
        bool SourceExists(IDataSource dataSource, Guid sourceId);

        /// <summary>
        /// Gets a publication source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="sourceId">The publication source identifier.</param>
        Source GetSource(IDataSource dataSource, Guid sourceId);

        /// <summary>
        /// Lists the publication sources.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Source> ListSources(IDataSource dataSource);

        /// <summary>
        /// Adds a publication source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="source">The publication source.</param>
        void AddSource(IDataSource dataSource, Source source);

        /// <summary>
        /// Deletes a publication source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="source">The publication source.</param>
        void DeleteSource(IDataSource dataSource, Source source);
    }
}
