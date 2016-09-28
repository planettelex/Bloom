using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the recording_session table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class RecordingSessionTable : ISqlTable
    {
        /// <summary>
        /// Gets the create recording_session table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE recording_session (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "song_id VARCHAR(36) NOT NULL , " +
                       "notes VARCHAR , " +
                       "occurred_on DATETIME NOT NULL , " +
                       "FOREIGN KEY (song_id) REFERENCES song(id) )";
            }
        }
    }
}
