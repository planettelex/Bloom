using System.Collections.Generic;
using System.Data.Linq;
using Bloom.Data.Interfaces;
using Bloom.Data.Tables;

namespace Bloom.Data.Services
{
    /// <summary>
    /// Service for managing library SQL tables.
    /// </summary>
    public class LibraryTableService : ITableService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryTableService"/> class.
        /// </summary>
        public LibraryTableService()
        {
            _tables = new List<ISqlTable>
            {
                new ActivityTable(),
                new AlbumTable(),
                new AlbumActivityTable()
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
