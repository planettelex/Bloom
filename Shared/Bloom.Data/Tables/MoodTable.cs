using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the mood table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class MoodTable : ISqlTable
    {
        /// <summary>
        /// Gets the create mood table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE mood (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "name VARCHAR NOT NULL )";
    }
}
