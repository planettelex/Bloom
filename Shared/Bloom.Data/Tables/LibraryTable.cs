using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class LibraryTable : ISqlTable
    {
        /// <summary>
        /// Gets the create library table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE library (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "name VARCHAR NOT NULL , " +
                       "folder_path VARCHAR NOT NULL , " +
                       "file_name VARCHAR NOT NULL , " +
                       "owner_id VARCHAR(36) NOT NULL , " +
                       "FOREIGN KEY (owner_id) REFERENCES person(id) )";
            }
        }
    }
}
