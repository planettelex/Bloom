using System;

namespace Bloom.Domain.Models
{
    public class Review
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime PublishedOn { get; set; }

        public Guid PublicationId { get; set; }

        public Publication Publication { get; set; }

        public Guid AuthorId { get; set; }

        public Person Author { get; set; }
    }
}
