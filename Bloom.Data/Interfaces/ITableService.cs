using System.Data.Linq;

namespace Bloom.Data.Interfaces
{
    /// <summary>
    /// Service for managing SQL tables.
    /// </summary>
    public interface ITableService
    {
        /// <summary>
        /// Creates the SQL tables.
        /// </summary>
        /// <param name="dataContext">A data context.</param>
        void CreateTables(DataContext dataContext);
    }
}
