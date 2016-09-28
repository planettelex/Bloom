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
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE mood (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "name VARCHAR NOT NULL )";
            }
        }
    }
}
