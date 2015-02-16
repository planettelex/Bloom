using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class LibraryPlaylistTable : ISqlTable
    {
        /// <summary>
        /// Gets the create library_playlist table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"library_playlist\" (" +
                       "\"playlist_id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"rating\" INTEGER )";
            }
        }
    }
}
