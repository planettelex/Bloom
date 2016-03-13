using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between an album and a release.
    /// </summary>
    [Table(Name = "album_release")]
    public class AlbumRelease : BindableBase
    {
        /// <summary>
        /// Creates a new album release instance.
        /// </summary>
        /// <param name="album">An album.</param>
        /// <param name="releaseDate">The release date.</param>
        public static AlbumRelease Create(Album album, DateTime releaseDate)
        {
            return new AlbumRelease
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                ReleaseDate = releaseDate
            };
        }

        /// <summary>
        /// Creates a new album release instance.
        /// </summary>
        /// <param name="album">An album.</param>
        /// <param name="releaseDate">The release date.</param>
        /// <param name="mediaTypes">The media types.</param>
        public static AlbumRelease Create(Album album, DateTime releaseDate, MediaTypes mediaTypes)
        {
            return new AlbumRelease
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                MediaTypes = mediaTypes,
                ReleaseDate = releaseDate
            };
        }

        /// <summary>
        /// Creates a new album release instance.
        /// </summary>
        /// <param name="album">An album.</param>
        /// <param name="releaseDate">The release date.</param>
        /// <param name="mediaTypes">The media types.</param>
        /// <param name="digitalFormats">The digital formats.</param>
        public static AlbumRelease Create(Album album, DateTime releaseDate, MediaTypes mediaTypes, DigitalFormats digitalFormats)
        {
            return new AlbumRelease
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                MediaTypes = mediaTypes,
                DigitalFormats = digitalFormats,
                ReleaseDate = releaseDate
            };
        }

        /// <summary>
        /// Creates a new album release instance.
        /// </summary>
        /// <param name="album">An album.</param>
        /// <param name="label">The label.</param>
        /// <param name="releaseDate">The release date.</param>
        /// <param name="mediaTypes">The media types.</param>
        /// <param name="digitalFormats">The digital formats.</param>
        /// <param name="catalogNumber">The catalog number.</param>
        public static AlbumRelease Create(Album album, DateTime releaseDate, MediaTypes mediaTypes, DigitalFormats digitalFormats, Label label, string catalogNumber)
        {
            return new AlbumRelease
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                LabelId = label.Id,
                MediaTypes = mediaTypes,
                DigitalFormats = digitalFormats,
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
        /// Gets or sets the album release media types.
        /// </summary>
        [Column(Name = "media_types")]
        public MediaTypes MediaTypes
        {
            get { return _mediaTypes; }
            set { SetProperty(ref _mediaTypes, value); }
        }
        private MediaTypes _mediaTypes;

        /// <summary>
        /// Gets or sets the album release digital formats.
        /// </summary>
        [Column(Name = "digital_formats")]
        public DigitalFormats DigitalFormats
        {
            get { return _digitalFormats; }
            set { SetProperty(ref _digitalFormats, value); }
        }
        private DigitalFormats _digitalFormats;

        /// <summary>
        /// Gets or sets the album release date.
        /// </summary>
        [Column(Name = "release_date")]
        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set { SetProperty(ref _releaseDate, value); }
        }
        private DateTime _releaseDate;

        /// <summary>
        /// Gets or sets the album release label identifier.
        /// </summary>
        [Column(Name = "label_id")]
        public Guid? LabelId { get; set; }

        /// <summary>
        /// Gets or sets the album release catalog number.
        /// </summary>
        [Column(Name = "catalog_number")]
        public string CatalogNumber
        {
            get { return _catalogNumber; }
            set { SetProperty(ref _catalogNumber, value); }
        }
        private string _catalogNumber;

    }
}
