using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a recording label.
    /// </summary>
    [Table(Name = "label")]
    public class Label
    {
        /// <summary>
        /// Gets or sets the label identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the label name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the label's bio.
        /// </summary>
        [Column(Name = "bio")]
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the label logo URL.
        /// </summary>
        [Column(Name = "logo_url")]
        public string LogoUrl { get; set; }

        /// <summary>
        /// Gets or sets the date on which the label was founded.
        /// </summary>
        [Column(Name = "founded_on")]
        public DateTime FoundedOn { get; set; }

        /// <summary>
        /// Gets or sets the date the label was closed.
        /// </summary>
        [Column(Name = "closed")]
        public DateTime? Closed { get; set; }

        /// <summary>
        /// Gets or sets the label personel.
        /// </summary>
        public List<LabelPersonel> Personel { get; set; }

        /// <summary>
        /// Gets or sets the label's releases.
        /// </summary>
        public List<AlbumRelease> Releases { get; set; } 
    }
}
