using System.Windows.Controls;

namespace Bloom.Controls
{
    /// <summary>
    /// A shell docking control tab.
    /// </summary>
    public class Tab
    {
        /// <summary>
        /// Gets or sets the tab header.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the tab content.
        /// </summary>
        public UserControl Content { get; set; }
    }
}
