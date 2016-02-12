using System;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface IPersonRepository
    {
        Person GetPerson(IDataSource dataSource, Guid personId);

        void AddPerson(IDataSource dataSource, Person person);

        void AddPersonPhoto(IDataSource dataSource, Person person, Photo photo, int priority);

        void AddPersonReference(IDataSource dataSource, Person person, Reference reference);

        void DeletePerson(IDataSource dataSource, Person person);
    }
}
