using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album_artwork table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AlbumArtworkTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_artwork table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE album_artwork (" +
                                   "album_id BLOB NOT NULL , " +
                                   "file_path VARCHAR NOT NULL , " +
                                   "priority INTEGER NOT NULL , " +
                                   "PRIMARY KEY (album_id, priority) , " +
                                   "FOREIGN KEY (album_id) REFERENCES album(id) )";
    }
}