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
        /// <param name="source">The review source.</param>
        /// <param name="url">The review URL.</param>
        public static Review Create(Source source, string url)
        {
            return new Review
            {
                Id = Guid.NewGuid(),
                Source = source,
                Url = url
            };
        }

        /// <summary>
        /// Creates a new review instance.
        /// </summary>
        /// <param name="source">The review source.</param>
        /// <param name="title">The review title.</param>
        /// <param name="body">The review body.</param>
        /// <param name="author">The review author.</param>
        public static Review Create(Source source, string title, string body, Person author)
        {
            return new Review
            {
                Id = Guid.NewGuid(),
                Source = source,
                Title = title,
                Body = body,
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
        /// Gets or sets the publication source identifier.
        /// </summary>
        [Column(Name = "source_id")]
        public Guid SourceId { get; set; }

        /// <summary>
        /// Gets or sets the publication source.
        /// </summary>
        public Source Source
        {
            get { return _source; }
            set
            {
                _source = value;
                SourceId = _source != null ? _source.Id : Guid.Empty;
            }
        }
        private Source _source;

        /// <summary>
        /// Gets or sets the author identifier.
        /// </summary>
        [Column(Name = "author_id")]
        public Guid? AuthorId { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public Person Author
        {
            get { return _author; }
            set
            {
                _author = value;
                AuthorId = _author != null ? _author.Id : (Guid?) null;
            }
        }
        private Person _author;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Url))
                return Url;

            if (!string.IsNullOrEmpty(Title))
                return Title;

            return Id.ToString();
        }
    }
}
