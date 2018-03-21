using Bloom.Data.Interfaces;

namespace Bloom.State.Data.Tables
{
    /// <summary>
    /// Represents the tab_library table.
    /// </summary>
    public class TabLibraryTable : ISqlTable
    {
        /// <summary>
        /// Gets the create tab_library table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE tab_library (" +
                                   "tab_id BLOB NOT NULL , " +
                                   "library_id BLOB NOT NULL , " +
                                   "PRIMARY KEY (tab_id, library_id) , " +
                                   "FOREIGN KEY (tab_id) REFERENCES tab(id) , " +
                                   "FOREIGN KEY (library_id) REFERENCES library_connection(library_id) ) ";
    }
}
