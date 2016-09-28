using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the source table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class SourceTable : ISqlTable
    {
        /// <summary>
        /// Gets the create source table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE source (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "name VARCHAR NOT NULL , " +
                       "website_url VARCHAR , " +
                       "type INTEGER NOT NULL )";
            }
        }
    }
}
