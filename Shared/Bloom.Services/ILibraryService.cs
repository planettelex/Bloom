using System.Collections.Generic;
using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    public interface ILibraryService
    {
        List<LibraryConnection> ListLibraryConnections();
    }
}
