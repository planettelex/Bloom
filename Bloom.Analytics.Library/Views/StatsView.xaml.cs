using Bloom.Analytics.Library.ViewModels;

namespace Bloom.Analytics.Library.Views
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
