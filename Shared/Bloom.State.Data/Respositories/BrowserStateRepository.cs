using System.Data.Linq;
using System.Linq;
using Bloom.Common;
using Bloom.Data.Interfaces;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Access methods for browser state data.
    /// </summary>
    /// <seealso cref="Bloom.State.Data.Respositories.IBrowserStateRepository" />
    public class BrowserStateRepository : IBrowserStateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserStateRepository" /> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="tabRepository">The tab repository.</param>
        public BrowserStateRepository(IDataSource dataSource, ILibraryConnectionRepository libraryConnectionRepository, ITabRepository tabRepository)
        {
            _dataSource = dataSource;
            _libraryConnectionRepository = libraryConnectionRepository;
            _tabRepository = tabRepository;
        }
        private readonly IDataSource _dataSource;
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;
        private readonly ITabRepository _tabRepository;
        private Table<BrowserState> BrowserStateTable { get { return _dataSource.Context.GetTable<BrowserState>(); } }

        /// <summary>
        /// Determines whether the browser state exists.
        /// </summary>
        /// <param name="user">The user.</param>
        public bool BrowserStateExists(User user)
        {
            if (!_dataSource.IsConnected() || user == null)
                return false;

            return BrowserStateTable.Any(u => u.UserId == user.PersonId);
        }

        /// <summary>
        /// Gets the browser state.
        /// </summary>
        /// <param name="user">The user.</param>
        public BrowserState GetBrowserState(User user)
        {
            if (!_dataSource.IsConnected() || user == null)
                return null;

            var stateQuery =
                from state in BrowserStateTable
                where state.UserId == user.PersonId
                select state;

            var browserState = stateQuery.SingleOrDefault();

            if (browserState != null)
            {
                browserState.User = user;
                browserState.Connections = _libraryConnectionRepository.ListLibraryConnections(true);
                browserState.Tabs = _tabRepository.ListTabs(ProcessType.Browser, user.PersonId);
            }
            
            return browserState;
        }

        /// <summary>
        /// Adds the browser state.
        /// </summary>
        /// <param name="browserState">The browser state.</param>
        public void AddBrowserState(BrowserState browserState)
        {
            if (!_dataSource.IsConnected() || browserState == null || browserState.User == null || BrowserStateExists(browserState.User))
                return;

            BrowserStateTable.InsertOnSubmit(browserState);
        }

        /// <summary>
        /// Deletes the anonymous state data.
        /// </summary>
        public void DeleteAnonymousBrowserState()
        {
            if (!_dataSource.IsConnected())
                return;

            var stateQuery =
                from state in BrowserStateTable
                where state.UserId == User.Anonymous.PersonId
                select state;

            var browserState = stateQuery.SingleOrDefault();
            if (browserState != null)
                BrowserStateTable.DeleteOnSubmit(browserState);
        }
    }
}
