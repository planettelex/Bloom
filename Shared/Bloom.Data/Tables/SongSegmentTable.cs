using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the song_segment table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class SongSegmentTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_segment table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE song_segment (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "song_id BLOB NOT NULL , " +
                                   "name VARCHAR , " +
                                   "start_time INTEGER NOT NULL , " +
                                   "stop_time INTEGER NOT NULL , " +
                                   "bpm INTEGER , " +
                                   "key INTEGER , " +
                                   "time_signature_id BLOB , " +
                                   "FOREIGN KEY (song_id) REFERENCES song(id) , " +
                                   "FOREIGN KEY (time_signature_id) REFERENCES time_signature(id) )";
    }
}