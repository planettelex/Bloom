using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class SongActivityTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_activity table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE song_activity (" +
                       "song_id VARCHAR(36) NOT NULL , " +
                       "activity_id VARCHAR(36) NOT NULL ," +
                       "PRIMARY KEY (song_id, activity_id) , " +
                       "FOREIGN KEY (song_id) REFERENCES song(id) , " +
                       "FOREIGN KEY (activity_id) REFERENCES activity(id) )";
            }
        }
    }
}
