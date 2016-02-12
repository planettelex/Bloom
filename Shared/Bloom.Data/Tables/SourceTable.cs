using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class SourceTable : ISqlTable
    {
        /// <summary>
        /// Gets the create publication table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE source (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "name VARCHAR NOT NULL , " +
                       "website_url VARCHAR , " +
                       "type INTEGER NOT NULL )";
            }
        }
    }
}
