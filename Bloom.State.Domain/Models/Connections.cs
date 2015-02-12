using System.Collections.Generic;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// The data connections.
    /// </summary>
    public class Connections
    {
        public Connections()
        {
            StateConnection = new StateConnection();
            LibraryConnections = new List<LibraryConnection>();
        }

        /// <summary>
        /// Gets or sets the state data connection.
        /// </summary>
        public StateConnection StateConnection { get; set; }

        /// <summary>
        /// Gets or sets the library data connections.
        /// </summary>
        public List<LibraryConnection> LibraryConnections { get; set; } 
    }
}
