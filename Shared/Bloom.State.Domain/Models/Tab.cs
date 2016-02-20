using System;
using System.Data.Linq.Mapping;
using Bloom.Common;
using Bloom.State.Domain.Enums;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// Represents a tab in a tabbed application.
    /// </summary>
    [Table(Name = "tab")]
    public class Tab
    {
        /// <summary>
        /// Creates a new tab instance.
        /// </summary>
        /// <param name="processType">The process creating the tab.</param>
        /// <param name="user">The user creating the tab.</param>
        /// <param name="buid">The tab's content identifier.</param>
        /// <param name="order">The tab's order.</param>
        /// <param name="tabType">The type of the tab.</param>
        /// <param name="header">The tab's header.</param>
        public static Tab Create(ProcessType processType, User user, Buid buid, int order, TabType tabType, string header)
        {
            return new Tab
            {
                Id = Guid.NewGuid(),
                UserId = user != null ? user.PersonId : Guid.Empty,
                Order = order,
                Type = tabType,
                Header = header,
                Process = processType,
                LibraryId = buid.LibraryId,
                EntityId = buid.EntityId
            };
        }

        /// <summary>
        /// Creates a new tab instance.
        /// </summary>
        /// <param name="processType">The process creating the tab.</param>
        /// <param name="user">The user creating the tab.</param>
        /// <param name="buid">The tab's content identifier.</param>
        /// <param name="order">The tab's order.</param>
        /// <param name="tabType">The type of the tab.</param>
        /// <param name="header">The tab's header.</param>
        /// <param name="view">The tab's view type.</param>
        public static Tab Create(ProcessType processType, User user, Buid buid, int order, TabType tabType, string header, string view)
        {
            return new Tab
            {
                Id = Guid.NewGuid(),
                UserId = user != null ? user.PersonId : Guid.Empty,
                Order = order,
                Type = tabType,
                Header = header,
                Process = processType,
                LibraryId = buid.LibraryId,
                EntityId = buid.EntityId,
                View = view
            };
        }

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
