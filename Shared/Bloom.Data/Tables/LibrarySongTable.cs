using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class LibrarySongTable : ISqlTable
    {
        /// <summary>
        /// Gets the create library_song table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"library_song\" (" +
                       "\"song_id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"rating\" INTEGER , " +
                       "\"notes\" VARCHAR , " +
                       "\"play_count\" INTEGER NOT NULL DEFAULT 0 , " +
                       "\"skip_count\" INTEGER NOT NULL DEFAULT 0 , " +
                       "\"remove_count\" INTEGER NOT NULL DEFAULT 0 , " +
                       "\"added_on\" DATETIME NOT NULL , " +
                       "\"rated_on\" DATETIME , " +
                       "\"last_played\" DATETIME , " +
                       "\"received_from_id\" VARCHAR(36) )";
            }
        }
    }
}
