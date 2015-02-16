using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class SongSegmentTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_segment table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"song_segment\" (" +
                       "\"id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"song_id\" VARCHAR(36) NOT NULL , " +
                       "\"start_time\" INTEGER NOT NULL , " +
                       "\"stop_time\" INTEGER NOT NULL , " +
                       "\"bpm\" INTEGER , " +
                       "\"key\" INTEGER , " +
                       "\"time_signature_id\" VARCHAR(36) )";
            }
        }
    }
}