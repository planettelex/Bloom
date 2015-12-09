using System;
using System.Windows.Controls;
using Bloom.State.Domain.Models;

namespace Bloom.Controls
{
    /// <summary>
    /// A shell docking control tab.
    /// </summary>
    public class TabControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabControl"/> class.
        /// </summary>
        public TabControl() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TabControl" /> class.
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <param name="content">The tab's content.</param>
        public TabControl(Tab tab, UserControl content)
        {
            Tab = tab;
            Content = content;
        }

        /// <summary>
        /// Gets the tab identifier.
        /// </summary>
        public Guid Id
        {
            get { return Tab == null ? Guid.Empty : Tab.Id; }
        }

        /// <summary>
        /// Gets the tab header.
        /// </summary>
        public string Header
        {
            get { return Tab == null ? string.Empty : Tab.Header; }
        }
        
        /// <summary>
        /// Gets or sets the tab data.
        /// </summary>
        public Tab Tab { get; set; }

        /// <summary>
        /// Gets or sets the tab content.
        /// </summary>
        public UserControl Content { get; set; }

        /// <summary>
        /// Gets a value indicating whether to show the view menu based on the tab type.
        /// </summary>
        public bool ShowViewMenu
        {
            get
            {
                if (Tab == null)
                    return false;

                switch (Tab.Type)
                {
                    case TabType.Library:
                        return true;
                    case TabType.Artist:
                        return true;
                    case TabType.Person:
                        return true;
                    default:
                        return false;
                }
            }
        }
    }
}
