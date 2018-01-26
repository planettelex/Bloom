using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the tag table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class TagTable : ISqlTable
    {
        /// <summary>
        /// Gets the create tag table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE tag (" +
                                   "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                                   "name VARCHAR NOT NULL )";
    }
}
