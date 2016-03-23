using System;
using System.Collections.Generic;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for filterset data.
    /// </summary>
    public interface IFiltersetRepository
    {
        /// <summary>
        /// Gets the filterset.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filtersetId">The filterset identifier.</param>
        Filterset GetFilterset(IDataSource dataSource, Guid filtersetId);

        /// <summary>
        /// Lists the filtersets.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        List<Filterset> ListFiltersets(IDataSource dataSource);

        /// <summary>
        /// Adds the filterset.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filterset">The filterset.</param>
        void AddFilterset(IDataSource dataSource, Filterset filterset);

        /// <summary>
        /// Adds a filterset element.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filtersetElement">The filterset element.</param>
        void AddFiltersetElement(IDataSource dataSource, FiltersetElement filtersetElement);

        /// <summary>
        /// Deletes a filterset element.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filtersetElement">The filterset element.</param>
        void DeleteFiltersetElement(IDataSource dataSource, FiltersetElement filtersetElement);

        /// <summary>
        /// Adds a filterset order.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filtersetOrder">The filterset order.</param>
        void AddFiltersetOrder(IDataSource dataSource, FiltersetOrder filtersetOrder);

        /// <summary>
        /// Deletes a filterset order.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filtersetOrder">The filterset order.</param>
        void DeleteFiltersetOrder(IDataSource dataSource, FiltersetOrder filtersetOrder);

        /// <summary>
        /// Deletes the filterset.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filterset">The filterset.</param>
        void DeleteFilterset(IDataSource dataSource, Filterset filterset);
    }
}
