using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class ReferenceTable : ISqlTable
    {
        /// <summary>
        /// Gets the create reference table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"reference\" (" +
                       "\"id\" VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "\"name\" VARCHAR NOT NULL , " +
                       "\"icon_url\" VARCHAR , " +
                       "\"url\" VARCHAR , " +
                       "\"title\" VARCHAR )";
            }
        }
    }
}
