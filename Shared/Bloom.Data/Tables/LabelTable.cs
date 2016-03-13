using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class LabelTable : ISqlTable
    {
        /// <summary>
        /// Gets the create label table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE label (" +
                       "id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "name VARCHAR NOT NULL , " +
                       "bio VARCHAR , " +
                       "logo_file_path VARCHAR , " +
                       "founded_on DATETIME , " +
                       "closed_on DATETIME , " +
                       "parent_label_id VARCHAR(36))";
            }
        }
    }
}
