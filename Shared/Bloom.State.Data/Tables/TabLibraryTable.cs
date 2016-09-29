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
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE tab_library (" +
                       "tab_id VARCHAR(36) NOT NULL , " +
                       "library_id VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (tab_id, library_id) , " +
                       "FOREIGN KEY (tab_id) REFERENCES tab(id) , " +
                       "FOREIGN KEY (library_id) REFERENCES library_connection(library_id) ) ";
            }
        }
    }
}
