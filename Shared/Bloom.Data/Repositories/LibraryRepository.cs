using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        public LibraryRepository(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        private readonly IPersonRepository _personRepository;

        public Library GetLibrary(IDataSource dataSource)
        {
            if (!dataSource.IsConnected())
                return null;

            var libraryTable = LibraryTable(dataSource);
            if (libraryTable == null)
                return null;

            var query =
                from library in libraryTable
                select library;

            var returnVal = query.SingleOrDefault();
            if (returnVal != null)
                returnVal.Owner = _personRepository.GetPerson(dataSource, returnVal.OwnerId);

            return returnVal;
        }

        public void AddLibrary(IDataSource dataSource, Library library)
        {
            if (!dataSource.IsConnected())
                return;

            var libraryTable = LibraryTable(dataSource);
            if (libraryTable == null)
                return;

            libraryTable.InsertOnSubmit(library);
        }

        private Table<Library> LibraryTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Library>() : null;
        }
    }
}
