using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class FiltersetStatementTable : ISqlTable
    {
        /// <summary>
        /// Gets the create filterset table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE filterset_statement (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "filter_id VARCHAR(36) NOT NULL , " +
                       "scope INTEGER NOT NULL DEFAULT 0 , " +
                       "comparison INTEGER NOT NULL DEFAULT 0 )";
            }
        }
    }
}
