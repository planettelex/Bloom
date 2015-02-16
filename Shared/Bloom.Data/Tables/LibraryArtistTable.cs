using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class LibraryArtistTable : ISqlTable
    {
        /// <summary>
        /// Gets the create library_artist table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE library_artist (" +
                       "artist_id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "rating INTEGER , " +
                       "notes VARCHAR , " +
                       "play_count INTEGER NOT NULL DEFAULT 0 , " +
                       "skip_count INTEGER NOT NULL DEFAULT 0 , " +
                       "remove_count INTEGER NOT NULL DEFAULT 0 , " +
                       "follow BOOL NOT NULL DEFAULT FALSE , " +
                       "FOREIGN KEY (artist_id) REFERENCES artist(id) )";
            }
        }
    }
}
