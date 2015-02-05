using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a person with a photo.
    /// </summary>
    public class PersonPhoto
    {
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the photo identifier.
        /// </summary>
        public Guid PhotoId { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public Photo Photo { get; set; }

        /// <summary>
        /// Gets or sets the photo order.
        /// </summary>
        public int Order { get; set; }
    }
}
