using Bloom.Analytics.LibraryModule.ViewModels;

namespace Bloom.Analytics.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for StatsView.xaml
    /// </summary>
    public partial class StatsView
    {
        public StatsView(StatsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
