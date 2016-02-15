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
        /// Creates a new review instance.
        /// </summary>
        /// <param name="url">The review URL.</param>
        public static Review Create(string url)
        {
            return new Review
            {
                Id = Guid.NewGuid(),
                Url = url
            };
        }

        /// <summary>
        /// Creates a new review instance.
        /// </summary>
        /// <param name="title">The review title.</param>
        /// <param name="body">The review body.</param>
        /// <param name="author">The review author.</param>
        public static Review Create(string title, string body, Person author)
        {
            return new Review
            {
                Id = Guid.NewGuid(),
                Title = title,
                Body = body,
                AuthorId = author.Id,
                Author = author
            };
        }

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
        public DateTime? PublishedOn { get; set; }

        /// <summary>
        /// Gets or sets the publication identifier.
        /// </summary>
        [Column(Name = "source_id")]
        public Guid SourceId { get; set; }

        /// <summary>
        /// Gets or sets the publication source.
        /// </summary>
        public Source Source { get; set; }

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
