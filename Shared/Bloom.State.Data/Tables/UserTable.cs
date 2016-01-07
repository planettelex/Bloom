using Bloom.Data.Interfaces;

namespace Bloom.State.Data.Tables
{
    public class UserTable : ISqlTable
    {
        /// <summary>
        /// Gets the create user table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"user\" (" +
                       "\"person_id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"name\" VARCHAR NOT NULL , " +
                       "\"birthday\" DATETIME , " +
                       "\"twitter\" VARCHAR , " +
                       "\"profile_image_url\" VARCHAR , " +
                       "\"last_login\" DATETIME)";
            }
        }
    }
}
