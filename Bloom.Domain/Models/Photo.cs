using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a photo.
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Gets or sets the photo identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the photo URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the date the photo was taken.
        /// </summary>
        public DateTime Taken { get; set; }
    }
}
