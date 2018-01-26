using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    /// <summary>
    /// Represents the label_personel_role table.
    /// </summary>
    /// <seealso cref="Bloom.Data.Interfaces.ISqlTable" />
    public class LabelPersonnelRoleTable : ISqlTable
    {
        /// <summary>
        /// Gets the create label_personel_role table SQL.
        /// </summary>
        public string CreateSql => "CREATE TABLE label_personnel_role (" +
                                   "label_personnel_id VARCHAR(36) NOT NULL , " +
                                   "role_id VARCHAR(36) NOT NULL , " +
                                   "PRIMARY KEY (label_personnel_id, role_id) , " +
                                   "FOREIGN KEY (label_personnel_id) REFERENCES label_personnel(id) , " +
                                   "FOREIGN KEY (role_id) REFERENCES role(id) )";
    }
}