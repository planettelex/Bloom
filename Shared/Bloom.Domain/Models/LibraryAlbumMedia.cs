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
        /// Creates a new library album media instance.
        /// </summary>
        /// <param name="libraryAlbum">The library album.</param>
        /// <param name="mediaType">The type of media.</param>
        public static LibraryAlbumMedia Create(LibraryAlbum libraryAlbum, MediaTypes mediaType)
        {
            return new LibraryAlbumMedia
            {
                LibraryId = libraryAlbum.LibraryId,
                AlbumId = libraryAlbum.AlbumId,
                Album = libraryAlbum.Album,
                MediaType = mediaType
            };
        }

        /// <summary>
        /// Creates a new library album media instance.
        /// </summary>
        /// <param name="libraryAlbum">The library album.</param>
        /// <param name="mediaType">The type of media.</param>
        /// <param name="release">The album release of this media.</param>
        /// <returns></returns>
        public static LibraryAlbumMedia Create(LibraryAlbum libraryAlbum, MediaTypes mediaType, AlbumRelease release)
        {
            return new LibraryAlbumMedia
            {
                LibraryId = libraryAlbum.LibraryId,
                AlbumId = libraryAlbum.AlbumId,
                Album = libraryAlbum.Album,
                MediaType = mediaType,
                ReleaseId = release.Id,
                Release = release
            };
        }

        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        public Guid LibraryId { get; set; }

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
        [Column(Name = "release_id")]
        public Guid ReleaseId { get; set; }

        /// <summary>
        /// Gets or sets the media release.
        /// </summary>
        public AlbumRelease Release { get; set; }
    }
}
