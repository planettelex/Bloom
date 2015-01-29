using System.Data.Linq;
using System.Linq;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Repository for the browser state.
    /// </summary>
    public class BrowserStateRepository : IBrowserStateRepository
    {
        public BrowserStateRepository(IStateDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IStateDataSource _dataSource;
        private Table<BrowserState> BrowserStateTable { get { return _dataSource.Context.GetTable<BrowserState>(); } }

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
    }
}
