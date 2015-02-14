using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class AlbumCreditRoleTable : ISqlTable
    {
        /// <summary>
        /// Gets the create album_credit_role table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"album_credit_role\" (" +
                       "\"album_credit_id\" VARCHAR(36) NOT NULL , " +
                       "\"role_id\" VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (\"album_credit_id\", \"role_id\") )";
            }
        }
    }
}