using System.Collections.Generic;

namespace Bloom.State.Domain.Models
{
    public class Connections
    {
        public StateConnection StateConnection { get; set; }

        public List<LibraryConnection> LibraryConnections { get; set; } 
    }
}
