using System;
using System.Collections.Generic;
using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    public interface ILibraryService
    {
        LibraryConnection GetLibraryConnection(Guid libraryId);

        LibraryConnection GetLibraryConnection(String filePath);

        List<LibraryConnection> ListLibraryConnections();

        bool ConnectLibrary(LibraryConnection libraryConnection, User user, bool timestamp, bool setLibrary);

        void ConnectLibraries(List<LibraryConnection> libraryConnections, User user, bool timestamp, bool setLibrary);

        void RemoveLibraryConnection(LibraryConnection libraryConnection);
    }
}
