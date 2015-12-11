using Bloom.Data.Interfaces;

namespace Bloom.State.Data.Tables
{
    /// <summary>
    /// The library_connection table.
    /// </summary>
    public class LibraryConnectionTable : ISqlTable
    {
        /// <summary>
        /// Gets the create library_connection table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"library_connection\" (" +
                       "\"library_id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"library_name\" VARCHAR NOT NULL , " +
                       "\"file_path\" VARCHAR NOT NULL , " +
                       "\"is_connected\" BOOL NOT NULL DEFAULT FALSE , " +
                       "\"last_connected\" DATETIME NOT NULL , " +
                       "\"last_connection_by\" VARCHAR(36) NOT NULL)";
            }
        }
    }
}
