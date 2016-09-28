using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the song_mood table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class SongMoodTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_mood table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE song_mood (" +
                       "song_id VARCHAR(36) NOT NULL , " +
                       "mood_id VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (song_id, mood_id) , " +
                       "FOREIGN KEY (song_id) REFERENCES song(id) , " +
                       "FOREIGN KEY (mood_id) REFERENCES mood(id) )";
            }
        }
    }
}