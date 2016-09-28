using Bloom.Data.Interfaces;

namespace Bloom.State.Data.Tables
{
    /// <summary>
    /// The tab library table.
    /// </summary>
    public class TabLibraryTable : ISqlTable
    {
        /// <summary>
        /// Gets the create tab library table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"tab_library\" (" +
                       "\"tab_id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"library_id\" VARCHAR(36) )";
            }
        }
    }
}
