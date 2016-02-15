using System;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public class FiltersetRepository : IFiltersetRepository
    {
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
                select element;

            filterset.Elements = filtersetElementsQuery.ToList();

            var filtersetOrderTable = FiltersetOrderTable(dataSource);
            var filtersetOrderQuery =
                from filtersetOrder in filtersetOrderTable
                where filtersetOrder.FiltersetId == filtersetId
                select filtersetOrder;

            filterset.Ordering = filtersetOrderQuery.ToList();

            return filterset;
        }

        public void AddFilterset(IDataSource dataSource, Filterset filterset)
        {
            if (!dataSource.IsConnected())
                return;

            var filtersetTable = FiltersetTable(dataSource);
            if (filtersetTable == null)
                return;

            filtersetTable.InsertOnSubmit(filterset);
        }

        public void AddFiltersetElement(IDataSource dataSource, FiltersetElement filtersetElement)
        {
            if (!dataSource.IsConnected())
                return;

            var filtersetElementTable = FiltersetElementTable(dataSource);
            if (filtersetElementTable == null)
                return;

            filtersetElementTable.InsertOnSubmit(filtersetElement);
        }

        public void AddFiltersetOrder(IDataSource dataSource, FiltersetOrder filtersetOrder)
        {
            if (!dataSource.IsConnected())
                return;

            var filtersetOrderTable = FiltersetOrderTable(dataSource);
            if (filtersetOrderTable == null)
                return;

            filtersetOrderTable.InsertOnSubmit(filtersetOrder);
        }

        public void DeleteFilterset(IDataSource dataSource, Filterset filterset)
        {
            if (!dataSource.IsConnected())
                return;

            var filtersetTable = FiltersetTable(dataSource);
            if (filtersetTable == null)
                return;

            filtersetTable.DeleteOnSubmit(filterset);
        }

        private static Table<Filterset> FiltersetTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Filterset>() : null;
        }

        private static Table<FiltersetElement> FiltersetElementTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<FiltersetElement>() : null;
        }

        private static Table<FiltersetOrder> FiltersetOrderTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<FiltersetOrder>() : null;
        }
    }
}
