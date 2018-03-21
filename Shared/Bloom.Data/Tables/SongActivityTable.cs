using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the song_activity table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class SongActivityTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_activity table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE song_activity (" +
                                   "song_id BLOB NOT NULL , " +
                                   "activity_id BLOB NOT NULL ," +
                                   "PRIMARY KEY (song_id, activity_id) , " +
                                   "FOREIGN KEY (song_id) REFERENCES song(id) , " +
                                   "FOREIGN KEY (activity_id) REFERENCES activity(id) )";
    }
}
