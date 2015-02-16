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
                return "CREATE TABLE song_credit_role (" +
                       "song_credit_id VARCHAR(36) NOT NULL , " +
                       "role_id VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (song_credit_id, role_id) , " +
                       "FOREIGN KEY (song_credit_id) REFERENCES song_credit(id) , " +
                       "FOREIGN KEY (role_id) REFERENCES role(id) )";
            }
        }
    }
}