using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class ArtistPhotoTable : ISqlTable
    {
        /// <summary>
        /// Gets the create artist_photo table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"artist_photo\" (" +
                       "\"artist_id\" VARCHAR(36) NOT NULL , " +
                       "\"photo_id\" VARCHAR(36) NOT NULL , " +
                       "\"order\" INTEGER NOT NULL , " +
                       "PRIMARY KEY (\"artist_id\", \"photo_id\") )";
            }
        }
    }
}