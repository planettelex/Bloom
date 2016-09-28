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
                       "tab_id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "library_id VARCHAR(36) )";
            }
        }
    }
}
