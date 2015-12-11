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
        /// Initializes a new instance of the <see cref="BrowserStateRepository"/> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public BrowserStateRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
        private Table<BrowserState> BrowserStateTable { get { return _dataSource.Context.GetTable<BrowserState>(); } }
        private Table<LibraryConnection> LibraryConnectionTable { get { return _dataSource.Context.GetTable<LibraryConnection>(); } }
        private Table<User> UserTable { get { return _dataSource.Context.GetTable<User>(); } } 
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

            var lastUserQuery =
                from users in UserTable
                orderby users.LastLogin descending
                select users;

            if (browserState != null)
                browserState.CurrentUser = lastUserQuery.ToList().FirstOrDefault();

            var connectionsQuery =
                from connections in LibraryConnectionTable
                where connections.IsConnected
                orderby connections.LastConnected descending
                select connections;

            if (browserState != null)
                browserState.Connections = connectionsQuery.ToList();

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
