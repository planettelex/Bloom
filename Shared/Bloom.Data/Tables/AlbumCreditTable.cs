using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class AlbumCreditTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_credit table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"album_credit\" (" +
                       "\"id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"album_id\" VARCHAR(36) NOT NULL , " +
                       "\"person_id\" VARCHAR(36) NOT NULL )";
            }
        }
    }
}