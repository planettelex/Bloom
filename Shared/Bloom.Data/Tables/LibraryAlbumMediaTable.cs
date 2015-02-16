using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class LibraryAlbumMediaTable : ISqlTable
    {
        /// <summary>
        /// Gets the create library_album_media table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"library_album_media\" (" +
                       "\"album_id\" VARCHAR(36) NOT NULL , " +
                       "\"media_type\" INTEGER NOT NULL , " +
                       "\"media_condition\" INTEGER NOT NULL , " +
                       "\"packaging_condition\" INTEGER NOT NULL , " +
                       "\"approximate_value\" REAL , " +
                       "\"purchased_price\" REAL , " +
                       "\"purchased_on\" DATETIME , " +
                       "\"on_loan_to_id\" VARCHAR(36) , " +
                       "\"release_id\" VARCHAR(36) , " +
                       "PRIMARY KEY (\"album_id\", \"media_type\") )";
            }
        }
    }
}
