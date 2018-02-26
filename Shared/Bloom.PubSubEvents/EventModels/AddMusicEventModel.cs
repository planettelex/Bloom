using System;
using System.Collections.Generic;
using Bloom.Domain.Enums;

namespace Bloom.Events.EventModels
{
    /// <summary>
    /// Event model for adding new music.
    /// </summary>
    public class AddMusicEventModel
    {
        /// <summary>
        /// Gets or sets the source to add music from.
        /// </summary>
        public MusicSource Source { get; set; }

        /// <summary>
        /// Gets or sets the path to the music source.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets whether to copy files to the library folder.
        /// </summary>
        public bool CopyFiles { get; set; }

        /// <summary>
        /// Gets or sets the library identifiers to add music to.
        /// </summary>
        public List<Guid> LibraryIds { get; set; } 
    }
}
