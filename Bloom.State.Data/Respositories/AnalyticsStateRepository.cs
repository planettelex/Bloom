using System.Linq;
using System.Data.Linq;
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
        public AnalyticsStateRepository(IStateDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IStateDataSource _dataSource;
        private Table<AnalyticsState> AnalyticsStateTable { get { return _dataSource.Context.GetTable<AnalyticsState>(); } }

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

            var query =
                from analyticsState in AnalyticsStateTable
                select analyticsState;

            return query.ToList().SingleOrDefault();
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
