using System;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface IPersonRepository
    {
        Person GetPerson(IDataSource dataSource, Guid personId);

        void AddPerson(IDataSource dataSource, Person person);
    }
}
