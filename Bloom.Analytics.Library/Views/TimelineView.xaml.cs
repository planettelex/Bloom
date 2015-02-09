using Bloom.Analytics.Library.ViewModels;

namespace Bloom.Analytics.Library.Views
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
