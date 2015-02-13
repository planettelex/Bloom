using Bloom.Analytics.LibraryModule.ViewModels;

namespace Bloom.Analytics.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for BarChartView.xaml
    /// </summary>
    public partial class BarChartView
    {
        public BarChartView(BarChartViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
