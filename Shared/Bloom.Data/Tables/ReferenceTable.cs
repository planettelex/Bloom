using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class ReferenceTable : ISqlTable
    {
        /// <summary>
        /// Gets the create reference table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE reference (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "source_id VARCHAR(36) , " +
                       "url VARCHAR NOT NULL , " +
                       "title VARCHAR ," +
                       "FOREIGN KEY (source_id) REFERENCES source(id) )";
            }
        }
    }
}
