using System.Collections.Generic;
using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    public interface ILibraryService
    {
        List<LibraryConnection> ListLibraryConnections();

        void ConnectLibrary(LibraryConnection libraryConnection, User user);

        void MakeLibraryConnections(List<LibraryConnection> libraryConnections, User user);
    }
}
