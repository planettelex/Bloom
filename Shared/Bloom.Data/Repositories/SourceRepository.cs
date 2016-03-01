using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for source data.
    /// </summary>
    public class SourceRepository : ISourceRepository
    {
        /// <summary>
        /// Gets a source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="sourceId">The source identifier.</param>
        public Source GetSource(IDataSource dataSource, Guid sourceId)
        {
            if (!dataSource.IsConnected())
                return null;

            var sourceTable = SourceTable(dataSource);
            if (sourceTable == null)
                return null;

            var sourceQuery =
                from s in sourceTable
                where s.Id == sourceId
                select s;

            return sourceQuery.SingleOrDefault();
        }

        /// <summary>
        /// Lists the sources.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public List<Source> ListSources(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var sourceTable = SourceTable(dataSource);
            if (sourceTable == null)
                return null;

            var sourcesQuery =
                from s in sourceTable
                orderby s.Name
                select s;

            return sourcesQuery.ToList();
        }

        /// <summary>
        /// Adds a source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="source">The source.</param>
        public void AddSource(IDataSource dataSource, Source source)
        {
            if (!dataSource.IsConnected())
                return;

            var sourceTable = SourceTable(dataSource);
            if (sourceTable == null)
                return;

            sourceTable.InsertOnSubmit(source);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="source">The source.</param>
        public void DeleteSource(IDataSource dataSource, Source source)
        {
            if (!dataSource.IsConnected())
                return;

            var sourceTable = SourceTable(dataSource);
            if (sourceTable == null)
                return;

            sourceTable.DeleteOnSubmit(source);
            dataSource.Save();
        }

        #region Tables

        private static Table<Source> SourceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Source>() : null;
        }

        #endregion
    }
}
