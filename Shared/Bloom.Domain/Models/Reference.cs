using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a reference to an external resource.
    /// </summary>
    [Table(Name = "reference")]
    public class Reference
    {
        /// <summary>
        /// Creates a new reference instance.
        /// </summary>
        /// <param name="url">The reference URL.</param>
        public static Reference Create(string url)
        {
            return new Reference
            {
                Id = Guid.NewGuid(),
                Url = url
            };
        }

        /// <summary>
        /// Creates a new reference instance.
        /// </summary>
        /// <param name="title">The reference title.</param>
        /// <param name="url">The reference URL.</param>
        public static Reference Create(string title, string url)
        {
            return new Reference
            {
                Id = Guid.NewGuid(),
                Title = title,
                Url = url
            };
        }

        /// <summary>
        /// Creates a new reference instance.
        /// </summary>
        /// <param name="source">The reference source.</param>
        /// <param name="url">The reference URL.</param>
        public static Reference Create(Source source, string url)
        {
            return new Reference
            {
                Id = Guid.NewGuid(),
                Url = url,
                Source = source
            };
        }

        /// <summary>
        /// Creates a new reference instance.
        /// </summary>
        /// <param name="source">The reference source.</param>
        /// <param name="title">The reference title.</param>
        /// <param name="url">The reference URL.</param>
        public static Reference Create(Source source, string title, string url)
        {
            return new Reference
            {
                Id = Guid.NewGuid(),
                Url = url,
                Title = title,
                Source = source
            };
        }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the source identifier.
        /// </summary>
        [Column(Name = "source_id")]
        public Guid? SourceId { get; set; }

        /// <summary>
        /// Gets or sets the reference URL.
        /// </summary>
        [Column(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the reference title.
        /// </summary>
        [Column(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        public Source Source
        {
            get { return _source; }
            set
            {
                _source = value;
                SourceId = _source != null ? _source.Id : (Guid?) null;
            }
        }
        private Source _source;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return Url;
        }
    }
}
