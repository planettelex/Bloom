using System;
using System.Windows;
using System.Windows.Controls;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Controls
{
    /// <summary>
    /// A shell docking control tab.
    /// </summary>
    public class TabControl : BindableBase
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
        public Guid TabId
        {
            get { return _tabId; }
            set { SetProperty(ref _tabId, value); }
        }
        private Guid _tabId;

        /// <summary>
        /// Gets or sets the tab header.
        /// </summary>
        public string Header
        {
            get { return _header; }
            set { SetProperty(ref _header, value); }
        }
        private string _header;

        /// <summary>
        /// Gets or sets the view menu visibility.
        /// </summary>
        public Visibility ViewMenuVisibility
        {
            get { return _viewMenuVisibility; }
            set { SetProperty(ref _viewMenuVisibility, value); }
        }
        private Visibility _viewMenuVisibility;

        /// <summary>
        /// Gets or sets the tab data.
        /// </summary>
        public Tab Tab
        {
            get { return _tab; }
            set
            {
                _tab = value;
                SetViewMenuVisiblity();
                Header = _tab != null ? _tab.Header : string.Empty;
                TabId = _tab != null ? _tab.Id : Guid.Empty;
            }
        }
        private Tab _tab;

        /// <summary>
        /// Gets or sets the tab content.
        /// </summary>
        public UserControl Content { get; set; }

        private void SetViewMenuVisiblity()
        {

            if (_tab == null)
                    return;

            switch (_tab.Type)
            {
                case TabType.Library:
                    ViewMenuVisibility = Visibility.Visible;
                    break;
                case TabType.Artist:
                    ViewMenuVisibility = Visibility.Visible;
                    break;
                case TabType.Person:
                    ViewMenuVisibility = Visibility.Visible;
                    break;
                default:
                    ViewMenuVisibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
