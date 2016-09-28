using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the person table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class PersonTable : ISqlTable
    {
        /// <summary>
        /// Gets the create person table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE person (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "name VARCHAR NOT NULL , " +
                       "born_on DATETIME , " +
                       "died_on DATETIME , " +
                       "from_city_id VARCHAR(36) , " +
                       "bio VARCHAR , " +
                       "twitter VARCHAR , " +
                       "follow BOOL NOT NULL DEFAULT FALSE )";
            }
        }
    }
}
