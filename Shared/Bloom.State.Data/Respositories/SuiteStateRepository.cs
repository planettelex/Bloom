using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Access methods for suite state data.
    /// </summary>
    /// <seealso cref="Bloom.State.Data.Respositories.ISuiteStateRepository" />
    public class SuiteStateRepository : ISuiteStateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuiteStateRepository"/> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public SuiteStateRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
        private Table<SuiteState> SuiteStateTable => _dataSource.Context.GetTable<SuiteState>();

        /// <summary>
        /// Determines whether a suite state exists.
        /// </summary>
        public bool SuiteStateExists()
        {
            if (!_dataSource.IsConnected())
                return false;

            return SuiteStateTable.Any();
        }

        /// <summary>
        /// Gets the suite state.
        /// </summary>
        public SuiteState GetSuiteState()
        {
            if (!_dataSource.IsConnected())
                return null;

            var stateQuery =
                from state in SuiteStateTable
                select state;

            var suiteState = stateQuery.SingleOrDefault();

            if (suiteState != null)
                _dataSource.Refresh(suiteState);

            return suiteState;
        }

        /// <summary>
        /// Adds the suite state.
        /// </summary>
        /// <param name="suiteState">The suite state.</param>
        public void AddSuiteState(SuiteState suiteState)
        {
            if (!_dataSource.IsConnected() || suiteState == null || SuiteStateExists())
                return;

            SuiteStateTable.InsertOnSubmit(suiteState);
        }
    }
}
