using System.Windows;

namespace Bloom.Analytics
{
    /// <summary>
    /// Interaction logic for TabHeader.xaml
    /// </summary>
    public partial class TabHeader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabHeader"/> class.
        /// </summary>
        public TabHeader()
        {
            InitializeComponent();
            ViewMenuVisibility = Visibility.Collapsed;
            DataContext = this;
        }

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
    }
}
