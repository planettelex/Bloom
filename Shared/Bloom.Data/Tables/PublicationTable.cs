using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class PublicationTable : ISqlTable
    {
        /// <summary>
        /// Gets the create publication table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE publication (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "name VARCHAR NOT NULL , " +
                       "website_url VARCHAR )";
            }
        }
    }
}
