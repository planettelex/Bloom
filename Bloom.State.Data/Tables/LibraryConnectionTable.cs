namespace Bloom.State.Data.Tables
{
    public class LibraryConnectionTable : ISqlTable
    {
        /// <summary>
        /// Gets the create library_connection table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"library_connection\" (" +
                       "\"library_id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"file_path\" VARCHAR NOT NULL )";
            }
        }
    }
}
