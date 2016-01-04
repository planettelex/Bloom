using System;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public Person GetPerson(IDataSource dataSource, Guid personId)
        {
            if (!dataSource.IsConnected())
                return null;

            var personTable = PersonTable(dataSource);
            if (personTable == null)
                return null;

            var query =
                from person in personTable
                where person.Id == personId
                select person;

            return query.SingleOrDefault();
        }

        public void AddPerson(IDataSource dataSource, Person person)
        {
            if (!dataSource.IsConnected())
                return;

            var personTable = PersonTable(dataSource);
            if (personTable == null)
                return;

            personTable.InsertOnSubmit(person);
        }

        private Table<Person> PersonTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Person>() : null;
        }
    }
}
