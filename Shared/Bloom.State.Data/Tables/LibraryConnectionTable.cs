using Bloom.Data.Interfaces;

namespace Bloom.State.Data.Tables
{
    /// <summary>
    /// Represents the library_connection table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class LibraryConnectionTable : ISqlTable
    {
        /// <summary>
        /// Gets the create library_connection table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE library_connection (" +
                       "library_id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "library_name VARCHAR NOT NULL , " +
                       "file_path VARCHAR NOT NULL , " +
                       "is_connected BOOL NOT NULL DEFAULT FALSE , " +
                       "owner_id VARCHAR(36) NOT NULL , " +
                       "owner_name VARCHAR NOT NULL , " +
                       "last_connected DATETIME NOT NULL , " +
                       "last_connection_by VARCHAR(36) NOT NULL )";
            }
        }
    }
}
