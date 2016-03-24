using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a song media.
    /// </summary>
    [Table(Name = "song_media")]
    public class SongMedia
    {
        /// <summary>
        /// Creates a new song media instance.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="mediaType">The type of the media.</param>
        public static SongMedia Create(Song song, MediaTypes mediaType)
        {
            return new SongMedia
            {
                Id = Guid.NewGuid(),
                SongId = song.Id,
                MediaType = mediaType
            };
        }

        /// <summary>
        /// Creates a new song media instance.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="digitalFormat">The digital format.</param>
        /// <param name="filePath">The file path.</param>
        public static SongMedia Create(Song song, DigitalFormats digitalFormat, string filePath)
        {
            return new SongMedia
            {
                Id = Guid.NewGuid(),
                SongId = song.Id,
                MediaType = MediaTypes.Digital,
                DigitalFormat = digitalFormat,
                FilePath = filePath
            };
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }
        
        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id")]
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets media type.
        /// </summary>
        [Column(Name = "media_type")]
        public MediaTypes MediaType { get; set; }

        /// <summary>
        /// Gets or sets the media digital format.
        /// </summary>
        [Column(Name = "digital_format")]
        public DigitalFormats? DigitalFormat { get; set; }

        /// <summary>
        /// Gets or sets the song media file path.
        /// </summary>
        [Column(Name = "file_path")]
        public string FilePath { get; set; }

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
        public int? VolumeOffset { get; set; }

        /// <summary>
        /// Gets or sets identifier of the person this media was received from.
        /// </summary>
        [Column(Name = "received_from_person_id")]
        public Guid ReceivedFromPersonId { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            var digitalMedia = string.IsNullOrEmpty(FilePath) ? DigitalFormat.ToString() : FilePath;
            return MediaType == MediaTypes.Digital ? digitalMedia : MediaType.ToString();
        }
    }
}
