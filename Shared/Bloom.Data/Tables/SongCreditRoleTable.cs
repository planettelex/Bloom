using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class SongCreditRoleTable : ISqlTable
    {
        /// <summary>
        /// Gets the create song_credit_role table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"song_credit_role\" (" +
                       "\"song_credit_id\" VARCHAR(36) NOT NULL , " +
                       "\"role_id\" VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (\"song_credit_id\", \"role_id\") )";
            }
        }
    }
}