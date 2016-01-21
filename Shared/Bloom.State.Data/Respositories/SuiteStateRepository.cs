using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    public class SuiteStateRepository : ISuiteStateRepository
    {
        public SuiteStateRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
        private Table<SuiteState> SuiteStateTable { get { return _dataSource.Context.GetTable<SuiteState>(); } }

        public SuiteState GetSuiteState()
        {
            if (!_dataSource.IsConnected())
                return null;

            var stateQuery =
                from state in SuiteStateTable
                select state;

            return stateQuery.SingleOrDefault();
        }

        public void AddSuiteState(SuiteState suiteState)
        {
            if (!_dataSource.IsConnected() || suiteState == null)
                return;

            SuiteStateTable.InsertOnSubmit(suiteState);
        }
    }
}
