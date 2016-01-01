using System.Collections.Generic;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    public class LibraryService : ILibraryService
    {
        public LibraryService(ILibraryConnectionRepository libraryConnectionRepository)
        {
            _libraryConnectionRepository = libraryConnectionRepository;
        }
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;

        public List<LibraryConnection> ListLibraryConnections()
        {
            var connected = _libraryConnectionRepository.ListLibraryConnections(true);
            var disconnected = _libraryConnectionRepository.ListLibraryConnections(false);

            var allConnections = connected ?? new List<LibraryConnection>();
            allConnections.AddRange(disconnected);

            return allConnections;
        }
    }
}
