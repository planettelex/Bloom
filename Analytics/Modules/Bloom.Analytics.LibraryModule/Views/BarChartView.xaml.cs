using Bloom.Analytics.Modules.LibraryModule.ViewModels;

namespace Bloom.Analytics.Modules.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for BarChartView.xaml
    /// </summary>
    public partial class BarChartView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BarChartView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public BarChartView(BarChartViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
