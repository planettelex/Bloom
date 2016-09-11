using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class ArtistTable : ISqlTable
    {
        /// <summary>
        /// Gets the create artist table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE artist (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "name VARCHAR NOT NULL , " +
                       "started_on DATETIME , " +
                       "ended_on DATETIME , " +
                       "formed_city_id VARCHAR(36) , " +
                       "bio VARCHAR , " +
                       "rating INTEGER , " +
                       "notes VARCHAR , " +
                       "twitter VARCHAR , " +
                       "is_solo BOOL NOT NULL DEFAULT FALSE , " +
                       "play_count INTEGER NOT NULL DEFAULT 0 , " +
                       "skip_count INTEGER NOT NULL DEFAULT 0 , " +
                       "remove_count INTEGER NOT NULL DEFAULT 0 , " +
                       "follow BOOL NOT NULL DEFAULT FALSE )";
            }
        }
    }
}
