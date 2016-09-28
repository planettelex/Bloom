using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the filterset_order table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class FiltersetOrderTable : ISqlTable
    {
        /// <summary>
        /// Gets the create filterset_order table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE filterset_order (" +
                       "filterset_id VARCHAR(36) NOT NULL , " +
                       "order_number INTEGER NOT NULL , " +
                       "order_id VARCHAR(36) NOT NULL , " +
                       "order_direction INTEGER NOT NULL DEFAULT 0 , " +
                       "PRIMARY KEY (filterset_id, order_number) , " +
                       "FOREIGN KEY (filterset_id) REFERENCES filterset(id) )";
            }
        }
    }
}