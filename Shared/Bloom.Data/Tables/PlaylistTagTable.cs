using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the playlist_tag table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class PlaylistTagTable : ISqlTable
    {
        /// <summary>
        /// Gets the create playlist_tag table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE playlist_tag (" +
                                   "playlist_id VARCHAR(36) NOT NULL , " +
                                   "tag_id VARCHAR(36) NOT NULL , " +
                                   "PRIMARY KEY (playlist_id, tag_id) , " +
                                   "FOREIGN KEY (playlist_id) REFERENCES playlist(id) , " +
                                   "FOREIGN KEY (tag_id) REFERENCES tag(id) )";
    }
}