using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class GenreTable : ISqlTable
    {
        /// <summary>
        /// Gets the create genre table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE genre (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "name VARCHAR NOT NULL , " +
                       "description VARCHAR , " +
                       "parent_genre_id VARCHAR(36) , " +
                       "FOREIGN KEY (parent_genre_id) REFERENCES genre(id) )";
            }
        }
    }
}
