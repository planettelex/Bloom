using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the library table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class LibraryTable : ISqlTable
    {
        /// <summary>
        /// Gets the create library table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE library (" +
                                   "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                                   "name VARCHAR NOT NULL , " +
                                   "folder_path VARCHAR NOT NULL , " +
                                   "file_name VARCHAR NOT NULL , " +
                                   "owner_id VARCHAR(36) NOT NULL , " +
                                   "owner_last_connected DATETIME NOT NULL, " +
                                   "FOREIGN KEY (owner_id) REFERENCES person(id) )";
    }
}
