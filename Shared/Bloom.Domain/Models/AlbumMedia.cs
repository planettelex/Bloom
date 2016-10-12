using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an album media.
    /// </summary>
    [Table(Name = "album_media")]
    public class AlbumMedia
    {
        /// <summary>
        /// Creates a new album media instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="mediaType">The type of the media.</param>
        public static AlbumMedia Create(Album album, MediaTypes mediaType)
        {
            return new AlbumMedia
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                MediaType = mediaType
            };
        }

        /// <summary>
        /// Creates a new album media instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="digitalFormats">The digital formats.</param>
        public static AlbumMedia Create(Album album, DigitalFormats digitalFormats)
        {
            return new AlbumMedia
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                MediaType = MediaTypes.Digital,
                DigitalFormats = digitalFormats
            };
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id", IsPrimaryKey = true)]
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        [Column(Name = "media_type", IsPrimaryKey = true)]
        public MediaTypes MediaType { get; set; }

        /// <summary>
        /// Gets or sets the media digital formats.
        /// </summary>
        [Column(Name = "digital_format")]
        public DigitalFormats? DigitalFormats { get; set; }

        /// <summary>
        /// Gets or sets the media condition.
        /// </summary>
        [Column(Name = "media_condition")]
        public Condition? MediaCondition { get; set; }

        /// <summary>
        /// Gets or sets the packaging condition.
        /// </summary>
        [Column(Name = "packaging_condition")]
        public Condition? PackagingCondition { get; set; }

        /// <summary>
        /// Gets or sets the approximate value of the media.
        /// </summary>
        [Column(Name = "approximate_value")]
        public decimal? ApproximateValue { get; set; }

        /// <summary>
        /// Gets or sets the purchased price of this media.
        /// </summary>
        [Column(Name = "purchased_price")]
        public decimal? PurchasedPrice { get; set; }

        /// <summary>
        /// Gets or sets date the media was purchased on.
        /// </summary>
        [Column(Name = "purchased_on")]
        public DateTime? PurchasedOn { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the person this media is on loan to.
        /// </summary>
        [Column(Name = "on_loan_to_person_id")]
        public Guid? OnLoanToPersonId { get; set; }

        /// <summary>
        /// Gets or sets the release identifier.
        /// </summary>
        [Column(Name = "release_id")]
        public Guid? ReleaseId { get; set; }

        /// <summary>
        /// Gets or sets the media release.
        /// </summary>
        public AlbumRelease Release
        {
            get { return _release; }
            set
            {
                _release = value;
                ReleaseId = _release != null ? _release.Id : (Guid?) null;
            }
        }
        private AlbumRelease _release;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return MediaType == MediaTypes.Digital ? DigitalFormats.ToString() : MediaType.ToString();
        }
    }
}
