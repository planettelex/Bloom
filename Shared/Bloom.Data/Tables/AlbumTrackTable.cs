using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album_track table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AlbumTrackTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_track table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE album_track (" +
                                   "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                                   "album_id VARCHAR(36) NOT NULL , " +
                                   "song_id VARCHAR(36) NOT NULL ," +
                                   "disc_number INTEGER NOT NULL DEFAULT 1 , " +
                                   "track_number INTEGER NOT NULL , " +
                                   "side VARCHAR(1) ," +
                                   "start_time INTEGER , " +
                                   "stop_time INTEGER , " +
                                   "FOREIGN KEY (album_id) REFERENCES album(id) , " +
                                   "FOREIGN KEY (song_id) REFERENCES song(id) )";
    }
}