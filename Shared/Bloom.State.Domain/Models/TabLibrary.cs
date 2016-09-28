using System;
using System.Data.Linq.Mapping;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// Represents a library associated with a tab for tabs with content from multiple libraries.
    /// </summary>
    [Table(Name = "tab_library")]
    public class TabLibrary
    {
        /// <summary>
        /// Creates a new tab library instance.
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <param name="libraryId">The library identifier.</param>
        public static TabLibrary Create(Tab tab, Guid libraryId)
        {
            return new TabLibrary
            {
                TabId = tab.Id,
                LibraryId = libraryId
            };
        }
        
        /// <summary>
        /// Gets or sets the tab identifier.
        /// </summary>
        [Column(Name = "tab_id")]
        public Guid TabId { get; set; }

        /// <summary>
        /// Gets or sets the library identifier.
        /// </summary>
        [Column(Name = "library_id")]
        public Guid LibraryId { get; set; }
    }
}
