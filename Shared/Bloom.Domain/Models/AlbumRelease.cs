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
