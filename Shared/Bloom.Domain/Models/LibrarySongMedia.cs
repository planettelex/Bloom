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
        /// Creates a new library song media instance.
        /// </summary>
        /// <param name="librarySong">The library song.</param>
        /// <param name="mediaType">The type of media.</param>
        /// <param name="uri">The song media URI.</param>
        public static LibrarySongMedia Create(LibrarySong librarySong, MediaTypes mediaType, string uri)
        {
            return new LibrarySongMedia
            {
                LibraryId = librarySong.LibraryId,
                SongId = librarySong.SongId,
                Song = librarySong.Song,
                MediaType = mediaType,
                Uri = uri
            };
        }

        /// <summary>
        /// Creates a new library song media instance.
        /// </summary>
        /// <param name="librarySong">The library song.</param>
        /// <param name="mediaType">The type of media.</param>
        /// <param name="digitalFormat">The song media digital format.</param>
        /// <param name="uri">The song media URI.</param>
        /// <returns></returns>
        public static LibrarySongMedia Create(LibrarySong librarySong, MediaTypes mediaType, DigitalFormats digitalFormat, string uri)
        {
            return new LibrarySongMedia
            {
                LibraryId = librarySong.LibraryId,
                SongId = librarySong.SongId,
                Song = librarySong.Song,
                MediaType = mediaType,
                DigitalFormat = digitalFormat,
                Uri = uri
            };
        }

        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        public Guid LibraryId { get; set; }

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
        [Column(Name = "digital_format")]
        public DigitalFormats DigitalFormat { get; set; }

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
