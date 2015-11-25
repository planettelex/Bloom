using System.Linq;
using System.Data.Linq;
using Bloom.Common;
using Bloom.Data.Interfaces;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Repository for the analytics state.
    /// </summary>
    public class AnalyticsStateRepository : IAnalyticsStateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsStateRepository"/> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public AnalyticsStateRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
        private Table<AnalyticsState> AnalyticsStateTable { get { return _dataSource.Context.GetTable<AnalyticsState>(); } }
        private Table<Tab> TabTable { get { return _dataSource.Context.GetTable<Tab>(); } } 

        /// <summary>
        /// Determines whether the analytics state exists.
        /// </summary>
        /// <returns></returns>
        public bool AnalyticsStateExists()
        {
            if (!_dataSource.IsConnected())
                return false;

            return AnalyticsStateTable.Any();
        }

        /// <summary>
        /// Gets the analytics state.
        /// </summary>
        public AnalyticsState GetAnalyticsState()
        {
            if (!_dataSource.IsConnected())
                return null;

            var stateQuery =
                from state in AnalyticsStateTable
                select state;

            var analyticsState = stateQuery.ToList().SingleOrDefault();

            var tabsQuery =
                from tabs in TabTable
                where tabs.Process == ProcessType.Analytics
                orderby tabs.Order
                select tabs;

            if (analyticsState != null)
                analyticsState.Tabs = tabsQuery.ToList();

            return analyticsState;
        }

        /// <summary>
        /// Adds the state of the analytics.
        /// </summary>
        /// <param name="analyticsState">State of the analytics.</param>
        public void AddAnalyticsState(AnalyticsState analyticsState)
        {
            if (!_dataSource.IsConnected() || AnalyticsStateExists())
                return;

            AnalyticsStateTable.InsertOnSubmit(analyticsState);
        }
    }
}
