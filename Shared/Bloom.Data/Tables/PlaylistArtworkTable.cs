using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the playlist_artwork table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class PlaylistArtworkTable : ISqlTable
    {
        /// <summary>
        /// Gets the create playlist_artwork table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE playlist_artwork (" +
                                   "playlist_id BLOB NOT NULL , " +
                                   "file_path VARCHAR NOT NULL ," +
                                   "priority INTEGER NOT NULL , " +
                                   "PRIMARY KEY (playlist_id, priority) , " +
                                   "FOREIGN KEY (playlist_id) REFERENCES playlist(id) )";
    }
}