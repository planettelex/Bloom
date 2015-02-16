using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class AlbumReleaseTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_release table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"album_release\" (" +
                       "\"id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"album_id\" VARCHAR(36) NOT NULL , " +
                       "\"media_types\" INTEGER NOT NULL DEFAULT 0, " +
                       "\"digital_formats\" INTEGER NOT NULL DEFAULT 0, " +
                       "\"release_date\" DATETIME , " +
                       "\"label_id\" VARCHAR(36) ," +
                       "\"catalog_number\" VARCHAR , " +
                       "FOREIGN KEY (album_id) REFERENCES album(id) , " +
                       "FOREIGN KEY (label_id) REFERENCES label(id) )";
            } 
        }
    }
}