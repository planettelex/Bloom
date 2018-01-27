using Bloom.Analytics.Modules.LibraryModule.ViewModels;

namespace Bloom.Analytics.Modules.LibraryModule.Views
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
