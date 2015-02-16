using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a library song with a media.
    /// </summary>
    [Table(Name = "library_song_media")]
    public class LibrarySongMedia
    {
        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id", IsPrimaryKey = true)]
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the song.
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// Gets or sets media type of this song media.
        /// </summary>
        [Column(Name = "media_type", IsPrimaryKey = true)]
        public MediaTypes MediaType { get; set; }

        /// <summary>
        /// Gets or sets the song media digital formats.
        /// </summary>
        [Column(Name = "digital_formats")]
        public DigitalFormats DigitalFormats { get; set; }

        /// <summary>
        /// Gets or sets the song media URI.
        /// </summary>
        [Column(Name = "uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets whether this song media is compressed.
        /// </summary>
        [Column(Name = "is_compressed")]
        public bool IsCompressed { get; set; }

        /// <summary>
        /// Gets or sets whether this song media is protected.
        /// </summary>
        [Column(Name = "is_protected")]
        public bool IsProtected { get; set; }

        /// <summary>
        /// Gets or sets whether this song media is damaged.
        /// </summary>
        [Column(Name = "is_damaged")]
        public bool IsDamaged { get; set; }

        /// <summary>
        /// Gets or sets the size of the media file, measured in kilobytes (KB).
        /// </summary>
        [Column(Name = "file_size")]
        public int? FileSize { get; set; }

        /// <summary>
        /// Gets or sets the media sample rate, measured in Hertz (Hz).
        /// </summary>
        [Column(Name = "sample_rate")]
        public int? SampleRate { get; set; }

        /// <summary>
        /// Gets or sets the media bit rate, measured in kilobits per second (kbps).
        /// </summary>
        [Column(Name = "bit_rate")]
        public int? BitRate { get; set; }

        /// <summary>
        /// Gets or sets the media volume offset.
        /// </summary>
        [Column(Name = "volume_offset")]
        public int VolumeOffset { get; set; }
    }
}
