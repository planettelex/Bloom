using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class TagTable : ISqlTable
    {
        /// <summary>
        /// Gets the create tag table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"tag\" (" +
                       "\"id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"name\" VARCHAR NOT NULL )";
            }
        }
    }
}
