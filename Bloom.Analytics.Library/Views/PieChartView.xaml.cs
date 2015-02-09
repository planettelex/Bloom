using Bloom.Analytics.Library.ViewModels;

namespace Bloom.Analytics.Library.Views
{
    /// <summary>
    /// Interaction logic for PieChartView.xaml
    /// </summary>
    public partial class PieChartView
    {
        public PieChartView(PieChartViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
