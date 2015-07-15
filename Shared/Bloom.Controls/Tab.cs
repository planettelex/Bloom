using System;
using System.Windows.Controls;

namespace Bloom.Controls
{
    /// <summary>
    /// A shell docking control tab.
    /// </summary>
    public class Tab
    {
        /// <summary>
        /// Gets or sets the tab identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        public Guid LibraryId { get; set; }

        /// <summary>
        /// Gets or sets the type of tab.
        /// </summary>
        public TabType Type { get; set; }

        /// <summary>
        /// Gets or sets the entity identifier contained in the tab.
        /// </summary>
        public Guid EntityId { get; set; }
        
        /// <summary>
        /// Gets or sets the tab header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show view menu on this tab.
        /// </summary>
        public bool ShowViewMenu { get; set; }

        /// <summary>
        /// Gets or sets the tab content.
        /// </summary>
        public UserControl Content { get; set; }
    }
}
