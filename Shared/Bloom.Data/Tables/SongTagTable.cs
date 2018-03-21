using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the song_tag table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class SongTagTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_tag table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE song_tag (" +
                                   "song_id BLOB NOT NULL , " +
                                   "tag_id BLOB NOT NULL , " +
                                   "PRIMARY KEY (song_id, tag_id) , " +
                                   "FOREIGN KEY (song_id) REFERENCES song(id) , " +
                                   "FOREIGN KEY (tag_id) REFERENCES tag(id) )";
    }
}