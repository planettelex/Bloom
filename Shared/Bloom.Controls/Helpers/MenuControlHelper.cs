using System.Collections.Generic;
using Telerik.Windows.Controls;

namespace Bloom.Controls.Helpers
{
    public class MenuControlHelper
    {
        /// <summary>
        /// Gets the sibling group items of the provided current item.
        /// </summary>
        /// <param name="menuItem">The tagged menu item to get the siblings of.</param>
        public static IEnumerable<RadMenuItem> GetSiblingGroupItems(RadMenuItem menuItem)
        {
            var parentItem = menuItem.ParentOfType<RadMenuItem>();
            if (parentItem == null)
                return null;

            var items = new List<RadMenuItem>();
            foreach (var item in parentItem.Items)
            {
                var container = parentItem.ItemContainerGenerator.ContainerFromItem(item) as RadMenuItem;
                if (container == null || container.Tag == null)
                    continue;

                if (container.Tag.Equals(menuItem.Tag))
                    items.Add(container);
            }
            return items;
        }
    }
}
