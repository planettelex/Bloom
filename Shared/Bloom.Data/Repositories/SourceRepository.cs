using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for publication source data.
    /// </summary>
    public class SourceRepository : ISourceRepository
    {
        /// <summary>
        /// Determines whether a publication source exists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="sourceId">The publication source identifier.</param>
        public bool SourceExists(IDataSource dataSource, Guid sourceId)
        {
            if (!dataSource.IsConnected())
                return false;

            var sourceTable = SourceTable(dataSource);
            if (sourceTable == null)
                return false;

            var sourceQuery =
                from s in sourceTable
                where s.Id == sourceId
                select s;

            return sourceQuery.Any();
        }

        /// <summary>
        /// Gets a publication source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="sourceId">The publication source identifier.</param>
        /// <returns></returns>
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
        /// Lists the publication sources.
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
        /// Adds a publication source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="source">The publication source.</param>
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
        /// Deletes a publication source.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="source">The publication source.</param>
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
            return dataSource?.Context.GetTable<Source>();
        }

        #endregion
    }
}
