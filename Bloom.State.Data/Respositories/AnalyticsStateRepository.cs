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
    }
}
