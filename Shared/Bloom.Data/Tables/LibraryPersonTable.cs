using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class LibraryPersonTable : ISqlTable
    {
        /// <summary>
        /// Gets the create library_person table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE library_person (" +
                       "person_id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE , " +
                       "follow BOOL NOT NULL DEFAULT FALSE , " +
                       "FOREIGN KEY (person_id) REFERENCES person(id) )";
            }
        }
    }
}
