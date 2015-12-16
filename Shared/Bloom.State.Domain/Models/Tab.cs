using System;
using System.Data.Linq.Mapping;
using Bloom.Common;

namespace Bloom.State.Domain.Models
{
    [Table(Name = "tab")]
    public class Tab
    {
        /// <summary>
        /// Gets or sets the tab identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the process the tab is for.
        /// </summary>
        [Column(Name = "process")]
        public ProcessType Process { get; set; }

        /// <summary>
        /// Gets or sets the tab user's person identifier.
        /// </summary>
        [Column(Name = "person_id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        [Column(Name = "library_id")]
        public Guid LibraryId { get; set; }

        /// <summary>
        /// Gets or sets the entity identifier contained in the tab.
        /// </summary>
        [Column(Name = "entity_id")]
        public Guid EntityId { get; set; }

        /// <summary>
        /// Gets or sets the type of tab.
        /// </summary>
        [Column(Name = "type")]
        public TabType Type { get; set; }

        /// <summary>
        /// Gets or sets the view inside the tab.
        /// </summary>
        [Column(Name = "view")]
        public string View { get; set; }

        /// <summary>
        /// Gets or sets the tab header.
        /// </summary>
        [Column(Name = "header")]
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the tab order.
        /// </summary>
        [Column(Name = "order")]
        public int Order { get; set; }
    }
}
