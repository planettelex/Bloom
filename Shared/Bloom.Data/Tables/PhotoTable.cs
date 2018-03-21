using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the photo table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class PhotoTable : ISqlTable
    {
        /// <summary>
        /// Gets the create photo table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE photo (" +
                                   "id BLOB PRIMARY KEY NOT NULL UNIQUE , " +
                                   "file_path VARCHAR NOT NULL , " +
                                   "caption VARCHAR , " +
                                   "taken_on DATETIME )";
    }
}
