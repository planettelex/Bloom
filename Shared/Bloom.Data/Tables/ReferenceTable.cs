using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the reference table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class ReferenceTable : ISqlTable
    {
        /// <summary>
        /// Gets the create reference table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE reference (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "source_id BLOB , " +
                                   "url VARCHAR NOT NULL , " +
                                   "title VARCHAR ," +
                                   "FOREIGN KEY (source_id) REFERENCES source(id) )";
    }
}
