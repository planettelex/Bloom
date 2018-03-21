using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the album_tag table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class AlbumTagTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_tag table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE album_tag (" +
                                   "album_id BLOB NOT NULL , " +
                                   "tag_id BLOB NOT NULL , " +
                                   "PRIMARY KEY (album_id, tag_id) , " +
                                   "FOREIGN KEY (album_id) REFERENCES album(id) , " +
                                   "FOREIGN KEY (tag_id) REFERENCES tag(id) )";
    }
}