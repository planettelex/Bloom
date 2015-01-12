using System;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    public class LibraryAlbumMedia
    {
        public Guid AlbumId { get; set; }

        public Album Album { get; set; }

        public MediaTypes MediaType { get; set; }

        public Condition MediaCondition { get; set; }

        public Condition PackagingCondition { get; set; }

        public decimal ApproximateValue { get; set; }

        public decimal PurchasedPrice { get; set; }

        public DateTime PurchasedOn { get; set; }

        public Guid OnLoanToId { get; set; }

        public Person OnLoadTo { get; set; }

        public Guid ReleaseId { get; set; }

        public AlbumRelease Release { get; set; }
    }
}
