using System.Data.Linq;

namespace Bloom.State.Data.Services
{
    /// <summary>
    /// Service for managing SQL tables.
    /// </summary>
    public interface IStateTableService
    {
        /// <summary>
        /// Creates the SQL tables.
        /// </summary>
        /// <param name="dataContext">A data context.</param>
        void CreateTables(DataContext dataContext);
    }
}
