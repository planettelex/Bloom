using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class PersonTable : ISqlTable
    {
        /// <summary>
        /// Gets the create person table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"person\" (" +
                       "\"id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"name\" VARCHAR NOT NULL , " +
                       "\"born_on\" DATETIME , " +
                       "\"died_on\" DATETIME , " +
                       "\"bio\" VARCHAR , " +
                       "\"twitter\" VARCHAR )";
            }
        }
    }
}
