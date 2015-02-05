using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a review.
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Gets or sets the review identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the review URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the review title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the review body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the date the review published on.
        /// </summary>
        public DateTime PublishedOn { get; set; }

        /// <summary>
        /// Gets or sets the publication identifier.
        /// </summary>
        public Guid PublicationId { get; set; }

        /// <summary>
        /// Gets or sets the publication.
        /// </summary>
        public Publication Publication { get; set; }

        /// <summary>
        /// Gets or sets the author identifier.
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public Person Author { get; set; }
    }
}
