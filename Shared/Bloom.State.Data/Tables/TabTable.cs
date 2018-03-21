using Bloom.Data.Interfaces;

namespace Bloom.State.Data.Tables
{
    /// <summary>
    /// Represents the tab table.
    /// </summary>
    public class TabTable : ISqlTable
    {
        /// <summary>
        /// Gets the create tab table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE tab (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "process INTEGER NOT NULL , " +
                                   "person_id BLOB NOT NULL , " +
                                   "library_id BLOB , " +
                                   "entity_id BLOB , " +
                                   "type INTEGER NOT NULL , " +
                                   "view VARCHAR , " +
                                   "header VARCHAR , " +
                                   "\"order\" INTEGER NOT NULL )";
    }
}
