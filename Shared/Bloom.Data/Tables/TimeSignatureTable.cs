using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class TimeSignatureTable : ISqlTable
    {
        /// <summary>
        /// Gets the create time_signature table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE time_signature (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "beats INTEGER NOT NULL , " +
                       "note_length INTEGER NOT NULL )";
            }
        }
    }
}
