using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the playlist_activity table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class PlaylistActivityTable : ISqlTable
    {
        /// <summary>
        /// Gets the create playlist_activity table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE playlist_activity (" +
                                   "playlist_id BLOB NOT NULL , " +
                                   "activity_id BLOB NOT NULL ," +
                                   "PRIMARY KEY (playlist_id, activity_id) , " +
                                   "FOREIGN KEY (playlist_id) REFERENCES playlist(id) , " +
                                   "FOREIGN KEY (activity_id) REFERENCES activity(id) )";
    }
}
