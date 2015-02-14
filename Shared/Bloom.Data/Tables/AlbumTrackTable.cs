using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class AlbumTrackTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_track table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"album_track\" (" +
                       "\"id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"album_id\" VARCHAR(36) NOT NULL , " +
                       "\"song_id\" VARCHAR(36) NOT NULL ," +
                       "\"disc_number\" INTEGER NOT NULL DEFAULT 1 , " +
                       "\"track_number\" INTEGER NOT NULL , " +
                       "\"start_time\" INTEGER , " +
                       "\"stop_time\" INTEGER ) ";
            }
        }
    }
}