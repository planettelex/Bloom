using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class RoleTable : ISqlTable
    {
        /// <summary>
        /// Gets the create role table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"role\" (" +
                       "\"id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"name\" VARCHAR NOT NULL )";
            }
        }
    }
}
