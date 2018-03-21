using Bloom.Data.Interfaces;

namespace Bloom.State.Data.Tables
{
    /// <summary>
    /// Represents the browser_state table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class BrowserStateTable : ISqlTable
    {
        /// <summary>
        /// Gets the create browser_state table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE browser_state (" +
                                   "process_name VARCHAR NOT NULL , " +
                                   "person_id BLOB NOT NULL , " +
                                   "skin_name VARCHAR NOT NULL , " +
                                   "window_state INTEGER NOT NULL , " +
                                   "sidebar_visible BOOL NOT NULL DEFAULT FALSE , " +
                                   "sidebar_width INTEGER NOT NULL , " +
                                   "selected_tab_id BLOB , " +
                                   "PRIMARY KEY (process_name, person_id) , " +
                                   "FOREIGN KEY (selected_tab_id) REFERENCES tab(id) )";
    }
}
