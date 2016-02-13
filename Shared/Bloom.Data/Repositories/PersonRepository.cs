using System;
using System.Collections.Generic;
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

        public void AddPerson(IDataSource dataSource, Person person)
        {
            if (!dataSource.IsConnected())
                return;

            var personTable = PersonTable(dataSource);
            if (personTable == null)
                return;

            personTable.InsertOnSubmit(person);
        }

        public void AddPersonPhoto(IDataSource dataSource, Person person, Photo photo, int priority)
        {
            if (!dataSource.IsConnected())
                return;

            var personPhotoTable = PersonPhotoTable(dataSource);
            if (personPhotoTable == null)
                return;

            var personPhoto = PersonPhoto.Create(person, photo, priority);
            personPhotoTable.InsertOnSubmit(personPhoto);
        }

        public void AddPersonReference(IDataSource dataSource, Person person, Reference reference)
        {
            if (!dataSource.IsConnected())
                return;

            var personReferenceTable = PersonReferenceTable(dataSource);
            if (personReferenceTable == null)
                return;

            var personReference = PersonReference.Create(person, reference);
            personReferenceTable.InsertOnSubmit(personReference);
        }

        public void DeletePerson(IDataSource dataSource, Person person)
        {
            if (!dataSource.IsConnected())
                return;

            var personTable = PersonTable(dataSource);
            if (personTable == null)
                return;

            personTable.DeleteOnSubmit(person);
        }

        private static Table<Person> PersonTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Person>() : null;
        }

        private static Table<PersonPhoto> PersonPhotoTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<PersonPhoto>() : null;
        }

        private static Table<PersonReference> PersonReferenceTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<PersonReference>() : null;
        }

        private static IEnumerable<Photo> PhotoTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Photo>() : null;
        }
    }
}
