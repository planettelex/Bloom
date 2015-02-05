using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a recording label.
    /// </summary>
    public class Label
    {
        /// <summary>
        /// Gets or sets the label identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the label name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the label's bio.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the label logo URL.
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// Gets or sets the date the label was founded.
        /// </summary>
        public DateTime Founded { get; set; }

        /// <summary>
        /// Gets or sets the date the label was closed.
        /// </summary>
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
