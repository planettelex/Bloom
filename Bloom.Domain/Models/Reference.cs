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
        /// Gets or sets the reference identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the reference name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the reference icon URL.
        /// </summary>
        [Column(Name = "icon_url")]
        public string IconUrl { get; set; }

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
    }
}
