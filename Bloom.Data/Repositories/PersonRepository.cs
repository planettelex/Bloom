using Bloom.Data.Interfaces;

namespace Bloom.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public PersonRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
    }
}
