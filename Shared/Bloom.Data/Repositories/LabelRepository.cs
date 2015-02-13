using Bloom.Data.Interfaces;

namespace Bloom.Data.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        public LabelRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
    }
}
