using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a library album with a media.
    /// </summary>
    [Table(Name = "library_album_media")]
    public class LibraryAlbumMedia
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id", IsPrimaryKey = true)]
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        [Column(Name = "media_type", IsPrimaryKey = true)]
        public MediaTypes MediaType { get; set; }

        /// <summary>
        /// Gets or sets the media condition.
        /// </summary>
        [Column(Name = "media_condition")]
        public Condition MediaCondition { get; set; }

        /// <summary>
        /// Gets or sets the packaging condition.
        /// </summary>
        [Column(Name = "packaging_condition")]
        public Condition PackagingCondition { get; set; }

        /// <summary>
        /// Gets or sets the approximate value of the media.
        /// </summary>
        [Column(Name = "approximate_value")]
        public decimal ApproximateValue { get; set; }

        /// <summary>
        /// Gets or sets the purchased price of this media.
        /// </summary>
        [Column(Name = "purchased_price")]
        public decimal PurchasedPrice { get; set; }

        /// <summary>
        /// Gets or sets date the media was purchased on.
        /// </summary>
        [Column(Name = "purchased_on")]
        public DateTime PurchasedOn { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the library owner this media is on loan to.
        /// </summary>
        [Column(Name = "on_loan_to_id")]
        public Guid OnLoanToId { get; set; }

        /// <summary>
        /// Gets or sets the library owner this media is on loan to.
        /// </summary>
        public Person OnLoadTo { get; set; }

        /// <summary>
        /// Gets or sets the media release identifier.
        /// </summary>
        [Column(Name = "released_id")]
        public Guid ReleaseId { get; set; }

        /// <summary>
        /// Gets or sets the media release.
        /// </summary>
        public AlbumRelease Release { get; set; }
    }
}
