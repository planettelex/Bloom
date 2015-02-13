using Bloom.Analytics.LibraryModule.ViewModels;

namespace Bloom.Analytics.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for TimelineView.xaml
    /// </summary>
    public partial class TimelineView
    {
        public TimelineView(TimelineViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
