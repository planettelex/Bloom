using System;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface IFiltersetRepository
    {
        Filterset GetFilterset(IDataSource dataSource, Guid filtersetId);

        void AddFilterset(IDataSource dataSource, Filterset filterset);

        void AddFiltersetElement(IDataSource dataSource, FiltersetElement filtersetElement);

        void AddFiltersetOrder(IDataSource dataSource, FiltersetOrder filtersetOrder);

        void DeleteFilterset(IDataSource dataSource, Filterset filterset);
    }
}
