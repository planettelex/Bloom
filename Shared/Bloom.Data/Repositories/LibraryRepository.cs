using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for library data.
    /// </summary>
    public class LibraryRepository : ILibraryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryRepository"/> class.
        /// </summary>
        /// <param name="personRepository">The person repository.</param>
        public LibraryRepository(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Gets the library.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public Library GetLibrary(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var personTable = PersonTable(dataSource);
            var libraryTable = LibraryTable(dataSource);
            if (libraryTable == null)
                return null;

            var libraryQuery =
                from l in libraryTable
                join person in personTable on l.OwnerId equals person.Id
                select new
                {
                    Library = l,
                    Person = person
                };

            var result = libraryQuery.SingleOrDefault();

            if (result == null)
                return null;

            var library = result.Library;
            if (library == null)
                return null;

            library.Owner = result.Person;

            return library;
        }

        /// <summary>
        /// Adds the library.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="library">The library.</param>
        public void AddLibrary(IDataSource dataSource, Library library)
        {
            if (!dataSource.IsConnected())
                return;

            if (!_personRepository.PersonExists(dataSource, library.OwnerId))
                _personRepository.AddPerson(dataSource, library.Owner);

            var libraryTable = LibraryTable(dataSource);
            if (libraryTable == null)
                return;

            libraryTable.InsertOnSubmit(library);
            dataSource.Save();
        }

        #region Tables

        private Table<Library> LibraryTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Library>() : null;
        }

        private Table<Person> PersonTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Person>() : null;
        }

        #endregion
    }
}
