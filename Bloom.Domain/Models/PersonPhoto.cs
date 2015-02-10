using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a person with a photo.
    /// </summary>
    [Table(Name = "person_photo")]
    public class PersonPhoto
    {
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        [Column(Name = "person_id", IsPrimaryKey = true)]
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the photo identifier.
        /// </summary>
        [Column(Name = "photo_id", IsPrimaryKey = true)]
        public Guid PhotoId { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public Photo Photo { get; set; }

        /// <summary>
        /// Gets or sets the photo order.
        /// </summary>
        [Column(Name = "order")]
        public int Order { get; set; }
    }
}
