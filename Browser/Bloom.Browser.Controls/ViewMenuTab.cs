using System.Windows.Controls;
using Bloom.Browser.Common;
using Bloom.State.Domain.Models;
using TabControl = Bloom.Controls.TabControl;

namespace Bloom.Browser.Controls
{
    /// <summary>
    /// A shell docking control tab with a view menu.
    /// </summary>
    public class ViewMenuTab : TabControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewMenuTab"/> class.
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <param name="content">The tab's content.</param>
        public ViewMenuTab(Tab tab, UserControl content)
        {
            Tab = tab;
            Content = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewMenuTab"/> class.
        /// </summary>
        /// <param name="viewType">Type of the view.</param>
        /// <param name="tab">The tab.</param>
        /// <param name="content">The content.</param>
        public ViewMenuTab(ViewType viewType, Tab tab, UserControl content)
        {
            ViewType = viewType;
            tab.View = viewType.ToString();
            Tab = tab;
            Content = content;
        }

        /// <summary>
        /// Gets or sets the view type.
        /// </summary>
        public ViewType ViewType { get; set; }
    }
}
