using Bloom.Data.Interfaces;

namespace Bloom.State.Data.Tables
{
    /// <summary>
    /// The tab table.
    /// </summary>
    public class TabTable : ISqlTable
    {
        /// <summary>
        /// Gets the create tab table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"tab\" (" +
                       "\"id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"process\" INTEGER NOT NULL , " +
                       "\"library_id\" VARCHAR(36) NOT NULL , " +
                       "\"entity_id\" VARCHAR(36) NOT NULL , " +
                       "\"type\" INTEGER NOT NULL , " +
                       "\"view\" VARCHAR , " +
                       "\"header\" VARCHAR, " +
                       "\"order\" INTEGER NOT NULL )";
            }
        }
    }
}
