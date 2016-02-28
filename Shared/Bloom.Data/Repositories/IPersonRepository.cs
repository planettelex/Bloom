using System;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for person data.
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Determines whether a person exists.
        /// </summary>
        /// <param name="data">The data source.</param>
        /// <param name="personId">The person identifier.</param>
        bool PersonExists(IDataSource data, Guid personId);
        
        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="personId">The person identifier.</param>
        Person GetPerson(IDataSource dataSource, Guid personId);

        /// <summary>
        /// Adds a person.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="person">The person.</param>
        void AddPerson(IDataSource dataSource, Person person);

        /// <summary>
        /// Adds a person photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="person">The person.</param>
        /// <param name="photo">The photo.</param>
        /// <param name="priority">The priority.</param>
        void AddPersonPhoto(IDataSource dataSource, Person person, Photo photo, int priority);

        /// <summary>
        /// Deletes a person.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="person">The person.</param>
        void DeletePerson(IDataSource dataSource, Person person);
    }
}
