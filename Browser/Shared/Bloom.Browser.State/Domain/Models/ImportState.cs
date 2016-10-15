using System.Collections.Generic;
using Bloom.Domain.Models;

namespace Bloom.Browser.State.Domain.Models
{
    /// <summary>
    /// Represents the state of an import.
    /// </summary>
    public class ImportState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportState"/> class.
        /// </summary>
        public ImportState()
        {
            Composers = new List<Person>();
        }

        /// <summary>
        /// Gets or sets the library being imported to.
        /// </summary>
        public Library Library { get; set; }

        /// <summary>
        /// Gets or sets the composers being imported.
        /// </summary>
        public List<Person> Composers { get; private set; }

        /// <summary>
        /// Gets or sets the genre being imported.
        /// </summary>
        public Genre Genre { get; set; }

        /// <summary>
        /// Gets or sets the artist being imported.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the album artist being imported.
        /// </summary>
        public Artist AlbumArtist { get; set; }

        /// <summary>
        /// Gets or sets the album being imported.
        /// </summary>
        public Album Album { get; set; }
    }
}
