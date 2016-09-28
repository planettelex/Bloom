using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album_track table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class PlaylistTrackTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_track table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE playlist_track (" +
                       "playlist_id VARCHAR(36) NOT NULL , " +
                       "song_id VARCHAR(36) NOT NULL ," +
                       "track_number INTEGER NOT NULL , " +
                       "PRIMARY KEY (playlist_id, song_id, track_number) , " +
                       "FOREIGN KEY (playlist_id) REFERENCES playlist(id) , " +
                       "FOREIGN KEY (song_id) REFERENCES song(id) )";
            }
        }
    }
}