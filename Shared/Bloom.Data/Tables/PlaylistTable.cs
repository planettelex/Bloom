using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the playlist table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class PlaylistTable : ISqlTable
    {
        /// <summary>
        /// Gets the create playlist table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE playlist (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "name VARCHAR NOT NULL , " +
                                   "description VARCHAR , " +
                                   "length INTEGER NOT NULL DEFAULT 0 , " +
                                   "rating INTEGER , " +
                                   "created_on DATETIME NOT NULL , " +
                                   "created_by_id BLOB NOT NULL , " +
                                   "FOREIGN KEY (created_by_id) REFERENCES person(id) )";
    }
}
