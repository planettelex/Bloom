using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for person data.
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository"/> class.
        /// </summary>
        /// <param name="photoRespository">The photo respository.</param>
        public PersonRepository(IPhotoRespository photoRespository)
        {
            _photoRespository = photoRespository;
        }
        private readonly IPhotoRespository _photoRespository;

        /// <summary>
        /// Determines whether a person exists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="personId">The person identifier.</param>
        public bool PersonExists(IDataSource dataSource, Guid personId)
        {
            if (!dataSource.IsConnected())
                return false;

            var personTable = PersonTable(dataSource);
            if (personTable == null)
                return false;

            var personQuery =
                from p in personTable
                where p.Id == personId
                select p;

            return personQuery.Any();
        }

        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="personId">The person identifier.</param>
        public Person GetPerson(IDataSource dataSource, Guid personId)
        {
            if (!dataSource.IsConnected())
                return null;

            var personTable = PersonTable(dataSource);
            if (personTable == null)
                return null;

            var personQuery =
                from p in personTable
                where p.Id == personId
                select p;

            var person = personQuery.SingleOrDefault();

            if (person == null)
                return null;

            var photoTable = PhotoTable(dataSource);
            var personPhotoTable = PersonPhotoTable(dataSource);
            var photosQuery =
                from pp in personPhotoTable
                join photo in photoTable on pp.PhotoId equals photo.Id
                where pp.PersonId == personId
                orderby pp.Priority
                select photo;

            person.Photos = photosQuery.ToList();

            return person;
        }

        /// <summary>
        /// Finds all people with the given name.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="personName">A person name.</param>
        public List<Person> FindPerson(IDataSource dataSource, string personName)
        {
            if (!dataSource.IsConnected())
                return null;

            var personTable = PersonTable(dataSource);
            if (personTable == null)
                return null;

            var photoTable = PhotoTable(dataSource);
            var personPhotoTable = PersonPhotoTable(dataSource);
            var personQuery =
                from p in personTable
                from pp in personPhotoTable.Where(t => p.Id == t.PersonId && t.Priority == 1).DefaultIfEmpty()
                from photo in photoTable.Where(h => pp.PhotoId == h.Id).DefaultIfEmpty()
                where p.Name.ToLower() == personName.ToLower()
                select new
                {
                    Person = p,
                    Photo = photo
                };

            var results = personQuery.ToList();
            if (!results.Any())
                return null;

            var matchingPeople = new List<Person>();
            foreach (var result in results)
            {
                var person = result.Person;
                person.ProfileImage = result.Photo;
                matchingPeople.Add(person);
            }

            return matchingPeople;
        }

        /// <summary>
        /// Adds a person.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="person">The person.</param>
        public void AddPerson(IDataSource dataSource, Person person)
        {
            if (!dataSource.IsConnected())
                return;

            var personTable = PersonTable(dataSource);
            if (personTable == null)
                return;

            personTable.InsertOnSubmit(person);
            dataSource.Save();
        }

        /// <summary>
        /// Adds a person photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="person">The person.</param>
        /// <param name="photo">The photo.</param>
        /// <param name="priority">The priority.</param>
        public void AddPersonPhoto(IDataSource dataSource, Person person, Photo photo, int priority)
        {
            if (!dataSource.IsConnected() || photo == null)
                return;

            if (!_photoRespository.PhotoExists(dataSource, photo.Id))
                _photoRespository.AddPhoto(dataSource, photo);

            var personPhotoTable = PersonPhotoTable(dataSource);
            if (personPhotoTable == null)
                return;

            var personPhoto = PersonPhoto.Create(person, photo, priority);
            personPhotoTable.InsertOnSubmit(personPhoto);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a person photo.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="person">The person.</param>
        /// <param name="photo">The photo.</param>
        public void DeletePersonPhoto(IDataSource dataSource, Person person, Photo photo)
        {
            if (!dataSource.IsConnected() || photo == null)
                return;

            var personPhotoTable = PersonPhotoTable(dataSource);
            if (personPhotoTable == null)
                return;

            var personPhotoQuery =
                from pp in personPhotoTable
                where pp.PersonId == person.Id && pp.PhotoId == photo.Id
                select pp;

            var personPhoto = personPhotoQuery.SingleOrDefault();
            if (personPhoto == null)
                return;

            personPhotoTable.DeleteOnSubmit(personPhoto);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a person.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="person">The person.</param>
        public void DeletePerson(IDataSource dataSource, Person person)
        {
            if (!dataSource.IsConnected())
                return;

            var personTable = PersonTable(dataSource);
            if (personTable == null)
                return;

            var personReferenceTable = PersonReferenceTable(dataSource);
            var personReferencesQuery =
                from pr in personReferenceTable
                where pr.PersonId == person.Id
                select pr;

            personReferenceTable.DeleteAllOnSubmit(personReferencesQuery.AsEnumerable());
            dataSource.Save();

            var personPhotoTable = PersonPhotoTable(dataSource);
            var personPhotosQuery =
                from pp in personPhotoTable
                where pp.PersonId == person.Id
                select pp;

            personPhotoTable.DeleteAllOnSubmit(personPhotosQuery.AsEnumerable());
            dataSource.Save();
                
            personTable.DeleteOnSubmit(person);
            dataSource.Save();
        }

        #region Tables

        private static Table<Person> PersonTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<Person>();
        }

        private static Table<PersonPhoto> PersonPhotoTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<PersonPhoto>();
        }

        private static Table<PersonReference> PersonReferenceTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<PersonReference>();
        }

        private static IEnumerable<Photo> PhotoTable(IDataSource dataSource)
        {
            return dataSource?.Context.GetTable<Photo>();
        }

        #endregion
    }
}
