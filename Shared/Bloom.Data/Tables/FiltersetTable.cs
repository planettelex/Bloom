using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the filterset table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class FiltersetTable : ISqlTable
    {
        /// <summary>
        /// Gets the create filterset table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE filterset (" +
                                   "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                                   "name VARCHAR , " +
                                   "created_on DATETIME NOT NULL )";
    }
}
