using Bloom.Analytics.Modules.LibraryModule.ViewModels;

namespace Bloom.Analytics.Modules.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for StatsView.xaml
    /// </summary>
    public partial class StatsView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatsView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public StatsView(StatsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
