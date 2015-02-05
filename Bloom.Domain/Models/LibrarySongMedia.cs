using System;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a library song with a media.
    /// </summary>
    public class LibrarySongMedia
    {
        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the song.
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// Gets or sets media type of this song media.
        /// </summary>
        public MediaTypes MediaType { get; set; }

        /// <summary>
        /// Gets or sets the song media digital format.
        /// </summary>
        public DigitalFormat? DigitalFormat { get; set; }

        /// <summary>
        /// Gets or sets the song media URI.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets whether this song media is compressed.
        /// </summary>
        public bool IsCompressed { get; set; }

        /// <summary>
        /// Gets or sets whether this song media is protected.
        /// </summary>
        public bool IsProtected { get; set; }

        /// <summary>
        /// Gets or sets whether this song media is damaged.
        /// </summary>
        public bool IsDamaged { get; set; }

        /// <summary>
        /// Gets or sets the size of the media file.
        /// </summary>
        public int? FileSize { get; set; }

        /// <summary>
        /// Gets or sets the media sample rate.
        /// </summary>
        public int? SampleRate { get; set; }

        /// <summary>
        /// Gets or sets the media bit rate.
        /// </summary>
        public int? BitRate { get; set; }

        /// <summary>
        /// Gets or sets the media volume offset.
        /// </summary>
        public int VolumeOffset { get; set; }
    }
}
