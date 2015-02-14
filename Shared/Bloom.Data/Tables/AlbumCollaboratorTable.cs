using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class AlbumCollaboratorTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_collaborator table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"album_collaborator\" (" +
                       "\"album_id\" VARCHAR(36) NOT NULL , " +
                       "\"artist_id\" VARCHAR(36) NOT NULL , " +
                       "\"url\" INTEGER NOT NULL ," +
                       "PRIMARY KEY (\"album_id\", \"artist_id\") )";
            }
        }
    }
}