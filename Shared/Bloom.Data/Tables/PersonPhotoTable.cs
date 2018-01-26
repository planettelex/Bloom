using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the person_photo table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class PersonPhotoTable : ISqlTable
    {
        /// <summary>
        /// Gets the create person_photo table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE person_photo (" +
                                   "person_id VARCHAR(36) NOT NULL , " +
                                   "photo_id VARCHAR(36) NOT NULL , " +
                                   "priority INTEGER NOT NULL , " +
                                   "PRIMARY KEY (person_id, photo_id, priority) , " +
                                   "FOREIGN KEY (person_id) REFERENCES person(id) , " +
                                   "FOREIGN KEY (photo_id) REFERENCES photo(id) )";
    }
}