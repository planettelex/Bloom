using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a photo.
    /// </summary>
    [Table(Name = "photo")]
    public class Photo
    {
        /// <summary>
        /// Gets or sets the photo identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the photo URL.
        /// </summary>
        [Column(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        [Column(Name = "caption")]
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the date the photo was taken on.
        /// </summary>
        [Column(Name = "taken_on")]
        public DateTime TakenOn { get; set; }
    }
}
