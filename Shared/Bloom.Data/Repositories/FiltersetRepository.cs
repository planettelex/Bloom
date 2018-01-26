using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for filterset data.
    /// </summary>
    public class FiltersetRepository : IFiltersetRepository
    {
        /// <summary>
        /// Gets the filterset.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filtersetId">The filterset identifier.</param>
        public Filterset GetFilterset(IDataSource dataSource, Guid filtersetId)
        {
            if (!dataSource.IsConnected())
                return null;

            var filtersetTable = FiltersetTable(dataSource);
            if (filtersetTable == null)
                return null;

            var filtersetQuery =
                from f in filtersetTable
                where f.Id == filtersetId
                select f;

            var filterset = filtersetQuery.SingleOrDefault();

            if (filterset == null)
                return null;

            var filtersetElementTable = FiltersetElementTable(dataSource);
            var filtersetElementsQuery =
                from element in filtersetElementTable
                where element.FiltersetId == filtersetId
                orderby  element.ElementNumber
                select element;

            filterset.Elements = filtersetElementsQuery.ToList();

            var filtersetOrderTable = FiltersetOrderTable(dataSource);
            var filtersetOrderQuery =
                from filtersetOrder in filtersetOrderTable
                where filtersetOrder.FiltersetId == filtersetId
                orderby filtersetOrder.OrderNumber
                select filtersetOrder;

            filterset.Ordering = filtersetOrderQuery.ToList();

            return filterset;
        }

        /// <summary>
        /// Lists the filtersets.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public List<Filterset> ListFiltersets(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var filtersetTable = FiltersetTable(dataSource);
            if (filtersetTable == null)
                return null;

            var filtersetQuery =
                from filterset in filtersetTable
                orderby filterset.CreatedOn
                select filterset;

            return filtersetQuery.ToList();
        }

        /// <summary>
        /// Adds the filterset.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filterset">The filterset.</param>
        public void AddFilterset(IDataSource dataSource, Filterset filterset)
        {
            if (!dataSource.IsConnected())
                return;

            var filtersetTable = FiltersetTable(dataSource);
            if (filtersetTable == null)
                return;

            filtersetTable.InsertOnSubmit(filterset);
            dataSource.Save();
        }

        /// <summary>
        /// Adds a filterset element.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filtersetElement">The filterset element.</param>
        public void AddFiltersetElement(IDataSource dataSource, FiltersetElement filtersetElement)
        {
            if (!dataSource.IsConnected())
                return;

            var filtersetElementTable = FiltersetElementTable(dataSource);
            if (filtersetElementTable == null)
                return;

            filtersetElementTable.InsertOnSubmit(filtersetElement);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a filterset element.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filtersetElement">The filterset element.</param>
        public void DeleteFiltersetElement(IDataSource dataSource, FiltersetElement filtersetElement)
        {
            if (!dataSource.IsConnected())
                return;

            var filtersetElementTable = FiltersetElementTable(dataSource);
            if (filtersetElementTable == null)
                return;

            filtersetElementTable.DeleteOnSubmit(filtersetElement);
            dataSource.Save();
        }

        /// <summary>
        /// Adds a filterset order.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filtersetOrder">The filterset order.</param>
        public void AddFiltersetOrder(IDataSource dataSource, FiltersetOrder filtersetOrder)
        {
            if (!dataSource.IsConnected())
                return;

            var filtersetOrderTable = FiltersetOrderTable(dataSource);
            if (filtersetOrderTable == null)
                return;

            filtersetOrderTable.InsertOnSubmit(filtersetOrder);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a filterset order.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filtersetOrder">The filterset order.</param>
        public void DeleteFiltersetOrder(IDataSource dataSource, FiltersetOrder filtersetOrder)
        {
            if (!dataSource.IsConnected())
                return;

            var filtersetOrderTable = FiltersetOrderTable(dataSource);
            if (filtersetOrderTable == null)
                return;

            filtersetOrderTable.DeleteOnSubmit(filtersetOrder);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the filterset.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="filterset">The filterset.</param>
        public void DeleteFilterset(IDataSource dataSource, Filterset filterset)
        {
            if (!dataSource.IsConnected())
                return;

            var filtersetTable = FiltersetTable(dataSource);
            if (filtersetTable == null)
                return;

            var filtersetElementTable = FiltersetElementTable(dataSource);
            var filtersetElementQuery =
                from fe in filtersetElementTable
                where fe.FiltersetId == filterset.Id
                select fe;

            filtersetElementTable.DeleteAllOnSubmit(filtersetElementQuery.AsEnumerable());
            dataSource.Save();

            var filtersetOrderTable = FiltersetOrderTable(dataSource);
            var filtersetOrderQuery =
                from fo in filtersetOrderTable
                where fo.FiltersetId == filterset.Id
                select fo;

            filtersetOrderTable.DeleteAllOnSubmit(filtersetOrderQuery.AsEnumerable());
            dataSource.Save();

            filtersetTable.DeleteOnSubmit(filterset);
            dataSource.Save();
        }

        #region Tables

        private static Table<Filterset> FiltersetTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<Filterset>();
        }

        private static Table<FiltersetElement> FiltersetElementTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<FiltersetElement>();
        }

        private static Table<FiltersetOrder> FiltersetOrderTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<FiltersetOrder>();
        }

        #endregion
    }
}
