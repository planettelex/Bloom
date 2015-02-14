using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class ArtistTable : ISqlTable
    {
        /// <summary>
        /// Gets the create artist table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"artist\" (" +
                       "\"id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"name\" VARCHAR NOT NULL , " +
                       "\"started\" DATETIME , " +
                       "\"ended\" DATETIME , " +
                       "\"bio\" VARCHAR , " +
                       "\"twitter\" VARCHAR , " +
                       "\"is_solo\" BOOL NOT NULL DEFAULT FALSE )";
            }
        }
    }
}
