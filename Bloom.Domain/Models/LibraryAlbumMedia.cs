using System;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a library album with a media.
    /// </summary>
    public class LibraryAlbumMedia
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        public MediaTypes MediaType { get; set; }

        /// <summary>
        /// Gets or sets the media condition.
        /// </summary>
        public Condition MediaCondition { get; set; }

        /// <summary>
        /// Gets or sets the packaging condition.
        /// </summary>
        public Condition PackagingCondition { get; set; }

        /// <summary>
        /// Gets or sets the approximate value of the media.
        /// </summary>
        public decimal ApproximateValue { get; set; }

        /// <summary>
        /// Gets or sets the purchased price of this media.
        /// </summary>
        public decimal PurchasedPrice { get; set; }

        /// <summary>
        /// Gets or sets date the media was purchased on.
        /// </summary>
        public DateTime PurchasedOn { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the library owner this media is on loan to.
        /// </summary>
        public Guid OnLoanToId { get; set; }

        /// <summary>
        /// Gets or sets the library owner this media is on loan to.
        /// </summary>
        public Person OnLoadTo { get; set; }

        /// <summary>
        /// Gets or sets the media release identifier.
        /// </summary>
        public Guid ReleaseId { get; set; }

        /// <summary>
        /// Gets or sets the media release.
        /// </summary>
        public AlbumRelease Release { get; set; }
    }
}
