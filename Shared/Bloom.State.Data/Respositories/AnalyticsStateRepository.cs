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
        /// Initializes a new instance of the <see cref="AnalyticsStateRepository" /> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="tabRepository">The tab repository.</param>
        public AnalyticsStateRepository(IDataSource dataSource, ILibraryConnectionRepository libraryConnectionRepository, ITabRepository tabRepository)
        {
            _dataSource = dataSource;
            _libraryConnectionRepository = libraryConnectionRepository;
            _tabRepository = tabRepository;
        }
        private readonly IDataSource _dataSource;
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;
        private readonly ITabRepository _tabRepository;
        private Table<AnalyticsState> AnalyticsStateTable { get { return _dataSource.Context.GetTable<AnalyticsState>(); } }

        /// <summary>
        /// Determines whether the analytics state exists.
        /// </summary>
        /// <param name="user">The user.</param>
        public bool AnalyticsStateExists(User user)
        {
            if (!_dataSource.IsConnected() || user == null)
                return false;

            return AnalyticsStateTable.Any(u => u.UserId == user.PersonId);
        }

        /// <summary>
        /// Gets the analytics state.
        /// </summary>
        public AnalyticsState GetAnalyticsState(User user)
        {
            if (!_dataSource.IsConnected() || user == null)
                return null;

            var stateQuery =
                from state in AnalyticsStateTable
                where state.UserId == user.PersonId
                select state;

            var analyticsState = stateQuery.SingleOrDefault();

            if (analyticsState != null)
            {
                analyticsState.User = user;
                analyticsState.Connections = _libraryConnectionRepository.ListLibraryConnections(true);
                analyticsState.Tabs = _tabRepository.ListTabs(ProcessType.Analytics, user.PersonId);
            }

            return analyticsState;
        }

        /// <summary>
        /// Adds the state of the analytics.
        /// </summary>
        /// <param name="analyticsState">State of the analytics.</param>
        public void AddAnalyticsState(AnalyticsState analyticsState)
        {
            if (!_dataSource.IsConnected() || analyticsState == null || AnalyticsStateExists(analyticsState.User))
                return;

            AnalyticsStateTable.InsertOnSubmit(analyticsState);
        }
    }
}
