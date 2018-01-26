using Bloom.Analytics.LibraryModule.ViewModels;

namespace Bloom.Analytics.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for PieChartView.xaml
    /// </summary>
    public partial class PieChartView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PieChartView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public PieChartView(PieChartViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
