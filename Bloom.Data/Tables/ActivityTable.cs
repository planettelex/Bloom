using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class ActivityTable : ISqlTable
    {
        /// <summary>
        /// Gets the create activity table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"activity\" (" +
                       "\"id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"name\" VARCHAR NOT NULL )";
            }
        }
    }
}
