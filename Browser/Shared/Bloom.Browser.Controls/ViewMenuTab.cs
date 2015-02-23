using Bloom.Browser.Common;
using Bloom.Controls;

namespace Bloom.Browser.Controls
{
    /// <summary>
    /// A shell docking control tab with a view menu.
    /// </summary>
    public class ViewMenuTab : Tab
    {
        /// <summary>
        /// Gets or sets the library view type.
        /// </summary>
        public ViewType ViewType { get; set; }
    }
}
