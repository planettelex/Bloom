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
        private SuiteState _suiteState;
        private readonly IDataSource _dataSource;
        private Table<SuiteState> SuiteStateTable { get { return _dataSource.Context.GetTable<SuiteState>(); } }

        public bool SuiteStateExists()
        {
            if (_suiteState != null)
                return true;

            if (!_dataSource.IsConnected())
                return false;

            return SuiteStateTable.Any();
        }

        public SuiteState GetSuiteState()
        {
            if (_suiteState != null)
                return _suiteState;

            if (!_dataSource.IsConnected())
                return null;

            var stateQuery =
                from state in SuiteStateTable
                select state;

            _suiteState = stateQuery.SingleOrDefault();

            if (_suiteState != null)
                _dataSource.Refresh(_suiteState);

            return _suiteState;
        }

        public void AddSuiteState(SuiteState suiteState)
        {
            if (!_dataSource.IsConnected() || suiteState == null || SuiteStateExists())
                return;

            _suiteState = suiteState;
            SuiteStateTable.InsertOnSubmit(_suiteState);
        }
    }
}
