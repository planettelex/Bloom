using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class LibraryAlbumTable : ISqlTable
    {
        /// <summary>
        /// Gets the create library_album table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"library_album\" (" +
                       "\"album_id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"rating\" INTEGER , " +
                       "\"review\" VARCHAR , " +
                       "\"parent_genre_id\" VARCHAR(36) )";
            }
        }
    }
}
