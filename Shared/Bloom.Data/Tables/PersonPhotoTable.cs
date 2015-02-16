using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class PersonPhotoTable : ISqlTable
    {
        /// <summary>
        /// Gets the create person_photo table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"person_photo\" (" +
                       "\"person_id\" VARCHAR(36) NOT NULL , " +
                       "\"photo_id\" VARCHAR(36) NOT NULL , " +
                       "\"order\" INTEGER NOT NULL , " +
                       "PRIMARY KEY (\"person_id\", \"photo_id\") )";
            }
        }
    }
}