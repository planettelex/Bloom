using Bloom.Analytics.LibraryModule.ViewModels;

namespace Bloom.Analytics.LibraryModule.Views
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
