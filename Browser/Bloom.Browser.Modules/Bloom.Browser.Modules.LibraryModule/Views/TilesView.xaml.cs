using Bloom.Browser.Modules.LibraryModule.ViewModels;

namespace Bloom.Browser.Modules.LibraryModule.Views
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
