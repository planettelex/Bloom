using Bloom.Data.Interfaces;

namespace Bloom.Data.Tables
{
    public class LabelPersonnelRoleTable : ISqlTable
    {
        /// <summary>
        /// Gets the create label_personel_role table SQL.
        /// </summary>
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE label_personnel_role (" +
                       "label_personel_id VARCHAR(36) NOT NULL , " +
                       "role_id VARCHAR(36) NOT NULL , " +
                       "PRIMARY KEY (label_personel_id, role_id) , " +
                       "FOREIGN KEY (label_personel_id) REFERENCES label_personel(id) , " +
                       "FOREIGN KEY (role_id) REFERENCES role(id) )";
            }
        }
    }
}