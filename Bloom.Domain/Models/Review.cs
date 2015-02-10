using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a review.
    /// </summary>
    [Table(Name = "review")]
    public class Review
    {
        /// <summary>
        /// Gets or sets the review identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the review URL.
        /// </summary>
        [Column(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the review title.
        /// </summary>
        [Column(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the review body.
        /// </summary>
        [Column(Name = "body")]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the date the review published on.
        /// </summary>
        [Column(Name = "published_on")]
        public DateTime PublishedOn { get; set; }

        /// <summary>
        /// Gets or sets the publication identifier.
        /// </summary>
        [Column(Name = "publication_id")]
        public Guid PublicationId { get; set; }

        /// <summary>
        /// Gets or sets the publication.
        /// </summary>
        public Publication Publication { get; set; }

        /// <summary>
        /// Gets or sets the author identifier.
        /// </summary>
        [Column(Name = "author_id")]
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public Person Author { get; set; }
    }
}
