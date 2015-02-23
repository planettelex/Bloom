using System;
using System.Windows;
using System.Windows.Input;
using Bloom.Browser.Common;
using Bloom.Browser.PubSubEvents;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser
{
    /// <summary>
    /// Interaction logic for TabHeader.xaml
    /// </summary>
    public partial class TabHeader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabHeader" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public TabHeader(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;
            ViewMenuVisibility = Visibility.Collapsed;
            DataContext = this;

            ChangeTabViewCommand = new DelegateCommand<string>(ChangeTabView, CanChangeTabView);
        }
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Gets or sets the tab identifier.
        /// </summary>
        public Guid TabId { get; set; }

        /// <summary>
        /// The text dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof (string), typeof (TabHeader), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// The view menu visibility dependency property.
        /// </summary>
        public static readonly DependencyProperty ViewMenuVisibilityProperty = DependencyProperty.Register("ViewMenuVisibility", typeof (Visibility), typeof(TabHeader), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the view menu visibility.
        /// </summary>
        public Visibility ViewMenuVisibility
        {
            get { return (Visibility) GetValue(ViewMenuVisibilityProperty); }
            set { SetValue(ViewMenuVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets or sets the change tab view command.
        /// </summary>
        public ICommand ChangeTabViewCommand { get; set; }

        private bool CanChangeTabView(string viewType)
        {
            return true;
        }

        private void ChangeTabView(string viewType)
        {
            var libraryViewType = (ViewType) Enum.Parse(typeof (ViewType), viewType);
            _eventAggregator.GetEvent<ChangeLibraryTabViewEvent>().Publish(new Tuple<Guid, ViewType>(TabId, libraryViewType));
        }
    }
}
