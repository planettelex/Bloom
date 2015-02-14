using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class AlbumArtworkTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_artwork table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"album_artwork\" (" +
                       "\"album_id\" VARCHAR(36) NOT NULL , " +
                       "\"order\" VARCHAR NOT NULL , " +
                       "\"url\" INTEGER NOT NULL ," +
                       "PRIMARY KEY (\"album_id\", \"order\") )";
            }
        }
    }
}