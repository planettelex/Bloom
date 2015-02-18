using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with a release.
    /// </summary>
    [Table(Name = "album_release")]
    public class AlbumRelease
    {
        /// <summary>
        /// Creates a new album release instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="releaseDate">The release date.</param>
        public static AlbumRelease Create(Album album, DateTime releaseDate)
        {
            return new AlbumRelease
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                Album = album,
                ReleaseDate = releaseDate
            };
        }

        /// <summary>
        /// Creates a new album release instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="releaseDate">The release date.</param>
        /// <param name="mediaTypes">The media types.</param>
        public static AlbumRelease Create(Album album, DateTime releaseDate, MediaTypes mediaTypes)
        {
            return new AlbumRelease
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                Album = album,
                MediaTypes = mediaTypes,
                ReleaseDate = releaseDate
            };
        }

        /// <summary>
        /// Creates a new album release instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="releaseDate">The release date.</param>
        /// <param name="mediaTypes">The media types.</param>
        /// <param name="digitalFormats">The digital formats.</param>
        public static AlbumRelease Create(Album album, DateTime releaseDate, MediaTypes mediaTypes, DigitalFormats digitalFormats)
        {
            return new AlbumRelease
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                Album = album,
                MediaTypes = mediaTypes,
                DigitalFormat = digitalFormats,
                ReleaseDate = releaseDate
            };
        }

        /// <summary>
        /// Creates a new album release instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="label">The label.</param>
        /// <param name="releaseDate">The release date.</param>
        /// <param name="mediaTypes">The media types.</param>
        /// <param name="digitalFormats">The digital formats.</param>
        /// <param name="catalogNumber">The catalog number.</param>
        public static AlbumRelease Create(Album album, Label label, DateTime releaseDate, MediaTypes mediaTypes, DigitalFormats digitalFormats, string catalogNumber)
        {
            return new AlbumRelease
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                Album = album,
                LabelId = label.Id,
                Label = label,
                MediaTypes = mediaTypes,
                DigitalFormat = digitalFormats,
                ReleaseDate = releaseDate,
                CatalogNumber = catalogNumber
            };
        }
        
        /// <summary>
        /// Gets or sets the release identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id")]
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the album release media types.
        /// </summary>
        [Column(Name = "media_types")]
        public MediaTypes MediaTypes { get; set; }

        /// <summary>
        /// Gets or sets the album release digital formats.
        /// </summary>
        [Column(Name = "digital_formats")]
        public DigitalFormats DigitalFormat { get; set; }

        /// <summary>
        /// Gets or sets the album release date.
        /// </summary>
        [Column(Name = "release_date")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the album release label identifier.
        /// </summary>
        [Column(Name = "label_id")]
        public Guid LabelId { get; set; }

        /// <summary>
        /// Gets or sets the album release label.
        /// </summary>
        public Label Label { get; set; }

        /// <summary>
        /// Gets or sets the album release catalog number.
        /// </summary>
        [Column(Name = "catalog_number")]
        public string CatalogNumber { get; set; }

    }
}
