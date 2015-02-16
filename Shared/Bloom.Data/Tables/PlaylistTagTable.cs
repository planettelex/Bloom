using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class PlaylistTagTable : ISqlTable
    {
        /// <summary>
        /// Gets the create playlist_tag table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"playlist_tag\" (" +
                       "\"playlist_id\" VARCHAR(36) NOT NULL , " +
                       "\"tag_id\" VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (\"playlist_id\", \"tag_id\") )";
            }
        }
    }
}