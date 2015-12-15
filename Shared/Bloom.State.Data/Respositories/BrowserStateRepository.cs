using System.Data.Linq;
using System.Linq;
using Bloom.Common;
using Bloom.Data.Interfaces;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Repository for the browser state.
    /// </summary>
    public class BrowserStateRepository : IBrowserStateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserStateRepository" /> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public BrowserStateRepository(IDataSource dataSource, ILibraryConnectionRepository libraryConnectionRepository, IUserRepository userRepository)
        {
            _dataSource = dataSource;
            _libraryConnectionRepository = libraryConnectionRepository;
            _userRepository = userRepository;
        }
        private readonly IDataSource _dataSource;
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;
        private readonly IUserRepository _userRepository;
        private Table<BrowserState> BrowserStateTable { get { return _dataSource.Context.GetTable<BrowserState>(); } }
        private Table<Tab> TabTable { get { return _dataSource.Context.GetTable<Tab>(); } } 

        /// <summary>
        /// Determines whether the browser state exists.
        /// </summary>
        /// <returns></returns>
        public bool BrowserStateExists()
        {
            if (!_dataSource.IsConnected())
                return false;

            return BrowserStateTable.Any();
        }

        /// <summary>
        /// Gets the browser state.
        /// </summary>
        /// <returns></returns>
        public BrowserState GetBrowserState()
        {
            if (!_dataSource.IsConnected())
                return null;

            var stateQuery =
                from state in BrowserStateTable
                select state;

            var browserState = stateQuery.SingleOrDefault();

            if (browserState != null)
            {
                browserState.Connections = _libraryConnectionRepository.ListLibraryConnections(true);
                browserState.CurrentUser = _userRepository.GetLastUser();
                
            }
                
            var tabsQuery =
                from tabs in TabTable
                where tabs.Process == ProcessType.Browser
                orderby tabs.Order
                select tabs;

            if (browserState != null)
                browserState.Tabs = tabsQuery.ToList();

            return browserState;
        }

        /// <summary>
        /// Adds the state of the browser.
        /// </summary>
        /// <param name="browserState">State of the browser.</param>
        public void AddBrowserState(BrowserState browserState)
        {
            if (!_dataSource.IsConnected() || BrowserStateExists())
                return;

            BrowserStateTable.InsertOnSubmit(browserState);
        }

        /// <summary>
        /// Adds the browser tab.
        /// </summary>
        /// <param name="tab">The tab.</param>
        public void AddBrowserTab(Tab tab)
        {
            if (!_dataSource.IsConnected())
                return;

            var tabsQuery =
                from tabs in TabTable
                where tabs.Process == ProcessType.Browser && tabs.Id == tab.Id
                select tabs;

            var existingTab = tabsQuery.SingleOrDefault();

            if (existingTab == null)
                TabTable.InsertOnSubmit(tab);
        }

        /// <summary>
        /// Removes the browser tab.
        /// </summary>
        /// <param name="tab">The tab.</param>
        public void RemoveBrowserTab(Tab tab)
        {
            if (!_dataSource.IsConnected())
                return;

            TabTable.DeleteOnSubmit(tab);
        }
    }
}
