using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public class LabelRepository : ILabelRepository
    {
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

            var personTable = PersonTable(dataSource);
            var labelPersonnelTable = LabelPersonnelTable(dataSource);
            var personnelQuery =
                from personnel in labelPersonnelTable
                join person in personTable on personnel.PersonId equals person.Id
                where personnel.LabelId == labelId
                orderby personnel.Priority
                select new LabelPersonnel
                {
                    Id = personnel.Id,
                    LabelId = labelId,
                    Priority = personnel.Priority,
                    Started = personnel.Started,
                    Ended = personnel.Ended,
                    PersonId = personnel.PersonId,
                    Person = new Person
                    {
                        Id = person.Id,
                        Name = person.Name,
                        Twitter = person.Twitter,
                        BornOn = person.BornOn,
                        DiedOn = person.DiedOn
                    }
                };

            label.Personnel = personnelQuery.ToList();

            if (label.Personnel == null)
                return label;

            var labelPersonnelRoleTable = LabelPersonnelRoleTable(dataSource);
            var roleTable = RoleTable(dataSource);
            foreach (var labelPersonnel in label.Personnel)
            {
                var lp = labelPersonnel;
                var rolesQuery =
                    from lpr in labelPersonnelRoleTable
                    join role in roleTable on lpr.RoleId equals role.Id
                    where lpr.LabelPersonelId == lp.Id
                    orderby role.Name
                    select new Role
                    {
                        Id = role.Id,
                        Name = role.Name
                    };

                labelPersonnel.Roles = rolesQuery.ToList();
            }

            return label;
        }

        public List<Label> ListLabels(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var labelTable = LabelTable(dataSource);
            if (labelTable == null)
                return null;

            var labelsQuery =
                from l in labelTable
                select l;

            return labelsQuery.ToList();
        }

        public void AddLabel(IDataSource dataSource, Label label)
        {
            if (!dataSource.IsConnected())
                return;

            var labelTable = LabelTable(dataSource);
            if (labelTable == null)
                return;

            labelTable.InsertOnSubmit(label);
        }

        public void AddLabelPersonnel(IDataSource dataSource, LabelPersonnel labelPersonnel)
        {
            if (!dataSource.IsConnected())
                return;

            var labelPersonnelTable = LabelPersonnelTable(dataSource);
            if (labelPersonnelTable == null)
                return;

            labelPersonnelTable.InsertOnSubmit(labelPersonnel);
        }

        public void DeleteLabel(IDataSource dataSource, Label label)
        {
            if (!dataSource.IsConnected())
                return;

            var labelTable = LabelTable(dataSource);
            if (labelTable == null)
                return;

            labelTable.DeleteOnSubmit(label);
        }

        private static Table<Label> LabelTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Label>() : null;
        }

        private static Table<LabelPersonnel> LabelPersonnelTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<LabelPersonnel>() : null;
        }

        private static Table<LabelPersonnelRole> LabelPersonnelRoleTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<LabelPersonnelRole>() : null;
        }

        private static Table<Role> RoleTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Role>() : null;
        }

        private static IEnumerable<Person> PersonTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Person>() : null;
        }
    }
}
