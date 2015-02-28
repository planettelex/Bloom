using System.Data.Linq;
using System.Linq;
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

            var query =
                from browserState in BrowserStateTable
                select browserState;

            return query.ToList().SingleOrDefault();
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
    }
}
