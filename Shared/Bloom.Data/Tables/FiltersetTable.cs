using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class FiltersetTable : ISqlTable
    {
        /// <summary>
        /// Gets the create filterset table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE filterset (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE )";
            }
        }
    }
}
