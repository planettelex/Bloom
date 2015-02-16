using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class PlaylistReferenceTable : ISqlTable
    {
        /// <summary>
        /// Gets the create playlist_reference table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"playlist_reference\" (" +
                       "\"playlist_id\" VARCHAR(36) NOT NULL , " +
                       "\"reference_id\" VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (\"playlist_id\", \"reference_id\") )";
            }
        }
    }
}