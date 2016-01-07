using System;
using System.Collections.Generic;
using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    public interface ILibraryService
    {
        LibraryConnection GetLibraryConnection(Guid libraryId, User user, bool makeConnection);

        List<LibraryConnection> ListLibraryConnections();

        void ConnectLibrary(LibraryConnection libraryConnection, User user);

        void MakeLibraryConnections(List<LibraryConnection> libraryConnections, User user);
    }
}
