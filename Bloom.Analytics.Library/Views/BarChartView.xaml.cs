using Bloom.Analytics.Library.ViewModels;

namespace Bloom.Analytics.Library.Views
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
