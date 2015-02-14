using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class FiltersetOrderingTable : ISqlTable
    {
        /// <summary>
        /// Gets the create filterset_element table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"filterset_ordering\" (" +
                       "\"filterset_id\" VARCHAR(36) NOT NULL , " +
                       "\"priority\" INTEGER NOT NULL , " +
                       "\"order_id\" VARCHAR(36) NOT NULL , " +
                       "\"scope\" INTEGER NOT NULL DEFAULT 0, " +
                       "\"direction\" INTEGER NOT NULL DEFAULT 0 , " +
                       "PRIMARY KEY (\"filterset_id\", \"priority\") )";
            }
        }
    }
}