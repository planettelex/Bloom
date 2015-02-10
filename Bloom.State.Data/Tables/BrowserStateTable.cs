using Bloom.Data;

namespace Bloom.State.Data.Tables
{
    public class BrowserStateTable : ISqlTable
    {
        /// <summary>
        /// Gets the create browser_state table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"browser_state\" (" +
                       "\"process_name\" VARCHAR PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"skin_name\" VARCHAR NOT NULL , " +
                       "\"window_state\" INTEGER NOT NULL , " +
                       "\"sidebar_width\" INTEGER NOT NULL , " +
                       "\"selected_tab_id\" VARCHAR(36) NOT NULL )";
            }
        }
    }
}
