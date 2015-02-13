using Bloom.Data.Interfaces;

namespace Bloom.Data.Repositories
{
    public class FiltersetRepository : IFiltersetRepository
    {
        public FiltersetRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
    }
}
