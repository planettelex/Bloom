using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for TilesView.xaml
    /// </summary>
    public partial class TilesView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TilesView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public TilesView(TilesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
