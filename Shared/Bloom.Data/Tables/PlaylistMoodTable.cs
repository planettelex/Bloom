using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the playlist_mood table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class PlaylistMoodTable : ISqlTable
    {
        /// <summary>
        /// Gets the create playlist_mood table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE playlist_mood (" +
                                   "playlist_id BLOB NOT NULL , " +
                                   "mood_id BLOB NOT NULL , " +
                                   "PRIMARY KEY (playlist_id, mood_id) , " +
                                   "FOREIGN KEY (playlist_id) REFERENCES playlist(id) , " +
                                   "FOREIGN KEY (mood_id) REFERENCES mood(id) )";
    }
}