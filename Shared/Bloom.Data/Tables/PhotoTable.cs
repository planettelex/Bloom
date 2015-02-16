using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class PhotoTable : ISqlTable
    {
        /// <summary>
        /// Gets the create photo table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE photo (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "url VARCHAR NOT NULL , " +
                       "caption VARCHAR , " +
                       "taken_on DATETIME )";
            }
        }
    }
}
