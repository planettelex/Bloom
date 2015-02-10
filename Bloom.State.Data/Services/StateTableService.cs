using System.Collections.Generic;
using System.Data.Linq;
using Bloom.Data.Interfaces;
using Bloom.State.Data.Tables;

namespace Bloom.State.Data.Services
{
    /// <summary>
    /// Service for managing SQL tables.
    /// </summary>
    public class StateTableService : ITableService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateTableService"/> class.
        /// </summary>
        public StateTableService()
        {
            _tables = new List<ISqlTable>
            {
                new LibraryConnectionTable(),
                new AnalyticsStateTable(),
                new BrowserStateTable(),
                new PlayerStateTable()
            };
        }
        private readonly List<ISqlTable> _tables;

        /// <summary>
        /// Creates the SQL tables.
        /// </summary>
        /// <param name="dataContext">A data context.</param>
        public void CreateTables(DataContext dataContext)
        {
            if (dataContext == null)
                return;

            foreach (var table in _tables)
                dataContext.ExecuteCommand(table.CreateSql);
        }
    }
}
