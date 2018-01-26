using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for label data.
    /// </summary>
    public class LabelRepository : ILabelRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRepository"/> class.
        /// </summary>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="personRepository">The person repository.</param>
        public LabelRepository(IRoleRepository roleRepository, IPersonRepository personRepository)
        {
            _roleRepository = roleRepository;
            _personRepository = personRepository;
        }
        private readonly IRoleRepository _roleRepository;
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="labelId">The label identifier.</param>
        public Label GetLabel(IDataSource dataSource, Guid labelId)
        {
            if (!dataSource.IsConnected())
                return null;

            var labelTable = LabelTable(dataSource);
            if (labelTable == null)
                return null;

            var labelQuery =
                from l in labelTable
                where l.Id == labelId
                select l;

            var label = labelQuery.SingleOrDefault();

            if (label == null)
                return null;

            
            var labelPersonnelTable = LabelPersonnelTable(dataSource);
            var personnelQuery =
                from personnel in labelPersonnelTable
                where personnel.LabelId == labelId
                orderby personnel.Priority
                select personnel;

            label.Personnel = personnelQuery.ToList();

            if (label.Personnel == null)
                return label;

            var labelPersonnelRoleTable = LabelPersonnelRoleTable(dataSource);
            var personTable = PersonTable(dataSource);
            var roleTable = RoleTable(dataSource);
            foreach (var labelPersonnel in label.Personnel)
            {
                var lp = labelPersonnel;
                var personQuery =
                    from person in personTable
                    where person.Id == lp.PersonId
                    select person;

                labelPersonnel.Person = personQuery.SingleOrDefault();

                var rolesQuery =
                    from lpr in labelPersonnelRoleTable
                    join role in roleTable on lpr.RoleId equals role.Id
                    where lpr.LabelPersonelId == lp.Id
                    orderby role.Name
                    select role;

                labelPersonnel.Roles = rolesQuery.ToList();
            }

            return label;
        }

        /// <summary>
        /// Lists the labels.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public List<Label> ListLabels(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var labelTable = LabelTable(dataSource);
            if (labelTable == null)
                return null;

            var labelsQuery =
                from label in labelTable
                orderby label.Name
                select label;

            return labelsQuery.ToList();
        }

        /// <summary>
        /// Adds a label.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="label">The label.</param>
        public void AddLabel(IDataSource dataSource, Label label)
        {
            if (!dataSource.IsConnected())
                return;

            var labelTable = LabelTable(dataSource);
            if (labelTable == null)
                return;

            labelTable.InsertOnSubmit(label);
            dataSource.Save();
        }

        /// <summary>
        /// Adds the label personnel.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="personnel">The label personnel.</param>
        public void AddLabelPersonnel(IDataSource dataSource, LabelPersonnel personnel)
        {
            if (!dataSource.IsConnected())
                return;

            if (!_personRepository.PersonExists(dataSource, personnel.PersonId))
                _personRepository.AddPerson(dataSource, personnel.Person);

            var labelPersonnelTable = LabelPersonnelTable(dataSource);
            if (labelPersonnelTable == null)
                return;

            labelPersonnelTable.InsertOnSubmit(personnel);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the label personnel.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="personnel">The label personnel.</param>
        public void DeleteLabelPersonnel(IDataSource dataSource, LabelPersonnel personnel)
        {
            if (!dataSource.IsConnected())
                return;

            var labelPersonnelTable = LabelPersonnelTable(dataSource);
            if (labelPersonnelTable == null)
                return;

            if (personnel.Roles != null && personnel.Roles.Any())
                foreach (var role in personnel.Roles)
                    DeleteLabelPersonnelRole(dataSource, personnel, role);

            labelPersonnelTable.DeleteOnSubmit(personnel);
            dataSource.Save();
        }

        /// <summary>
        /// Adds the label personnel role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="personnel">The label personnel.</param>
        /// <param name="role">The role.</param>
        public void AddLabelPersonnelRole(IDataSource dataSource, LabelPersonnel personnel, Role role)
        {
            if (!dataSource.IsConnected())
                return;

            if (!_roleRepository.RoleExists(dataSource, role.Id))
                _roleRepository.AddRole(dataSource, role);

            var labelPersonnelRoleTable = LabelPersonnelRoleTable(dataSource);
            if (labelPersonnelRoleTable == null)
                return;

            var artistMemberRole = LabelPersonnelRole.Create(personnel, role);

            labelPersonnelRoleTable.InsertOnSubmit(artistMemberRole);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes the label personnel role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="personnel">The label personnel.</param>
        /// <param name="role">The role.</param>
        public void DeleteLabelPersonnelRole(IDataSource dataSource, LabelPersonnel personnel, Role role)
        {
            if (!dataSource.IsConnected())
                return;

            var labelPersonnelRoleTable = LabelPersonnelRoleTable(dataSource);
            if (labelPersonnelRoleTable == null)
                return;

            var labelPersonnelRoleQuery =
                from lpr in labelPersonnelRoleTable
                where lpr.LabelPersonelId == personnel.Id && lpr.RoleId == role.Id
                select lpr;

            var labelPersonnelRole = labelPersonnelRoleQuery.SingleOrDefault();
            if (labelPersonnelRole == null)
                return;

            labelPersonnelRoleTable.DeleteOnSubmit(labelPersonnelRole);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a label.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="label">The label.</param>
        public void DeleteLabel(IDataSource dataSource, Label label)
        {
            if (!dataSource.IsConnected())
                return;

            var labelTable = LabelTable(dataSource);
            if (labelTable == null)
                return;

            var labelPersonnelTable = LabelPersonnelTable(dataSource);
            var labelPersonnelQuery =
                from lp in labelPersonnelTable
                where lp.LabelId == label.Id
                select lp;

            var personnel = labelPersonnelQuery.ToList();
            foreach (var labelPersonnel in personnel)
            {
                var lp = labelPersonnel;
                var labelPersonnelRoleTable = LabelPersonnelRoleTable(dataSource);
                var labelPersonnelRoleQuery =
                    from lpr in labelPersonnelRoleTable
                    where lpr.LabelPersonelId == lp.Id
                    select lpr;

                labelPersonnelRoleTable.DeleteAllOnSubmit(labelPersonnelRoleQuery.AsEnumerable());
                dataSource.Save();

                labelPersonnelTable.DeleteOnSubmit(labelPersonnel);
                dataSource.Save();
            }

            labelTable.DeleteOnSubmit(label);
            dataSource.Save();
        }

        #region Tables

        private static Table<Label> LabelTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<Label>();
        }

        private static Table<LabelPersonnel> LabelPersonnelTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<LabelPersonnel>();
        }

        private static Table<LabelPersonnelRole> LabelPersonnelRoleTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<LabelPersonnelRole>();
        }

        private static Table<Role> RoleTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<Role>();
        }

        private static Table<Person> PersonTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<Person>();
        }

        #endregion
    }
}
