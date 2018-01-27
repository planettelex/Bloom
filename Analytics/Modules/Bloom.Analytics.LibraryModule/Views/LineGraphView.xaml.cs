using Bloom.Analytics.Modules.LibraryModule.ViewModels;

namespace Bloom.Analytics.Modules.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for LineGraphView.xaml
    /// </summary>
    public partial class LineGraphView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LineGraphView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public LineGraphView(LineGraphViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
