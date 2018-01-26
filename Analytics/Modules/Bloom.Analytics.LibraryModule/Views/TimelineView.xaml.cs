using Bloom.Analytics.LibraryModule.ViewModels;

namespace Bloom.Analytics.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for TimelineView.xaml
    /// </summary>
    public partial class TimelineView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public TimelineView(TimelineViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
