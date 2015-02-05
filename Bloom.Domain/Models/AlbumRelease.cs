using System;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with a release.
    /// </summary>
    public class AlbumRelease
    {
        /// <summary>
        /// Gets or sets the release identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the album release media types.
        /// </summary>
        public MediaTypes MediaTypes { get; set; }

        /// <summary>
        /// Gets or sets the album release digital format.
        /// </summary>
        public DigitalFormat? DigitalFormat { get; set; }

        /// <summary>
        /// Gets or sets the album release date.
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the album release label identifier.
        /// </summary>
        public Guid LabelId { get; set; }

        /// <summary>
        /// Gets or sets the album release label.
        /// </summary>
        public Label Label { get; set; }

        /// <summary>
        /// Gets or sets the album release catalog number.
        /// </summary>
        public string CatalogNumber { get; set; }

    }
}
