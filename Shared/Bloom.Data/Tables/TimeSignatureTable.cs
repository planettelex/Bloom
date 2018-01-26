using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the time_signature table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class TimeSignatureTable : ISqlTable
    {
        /// <summary>
        /// Gets the create time_signature table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE time_signature (" +
                                   "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                                   "beats_per_measure INTEGER NOT NULL , " +
                                   "beat_length INTEGER NOT NULL )";
    }
}
