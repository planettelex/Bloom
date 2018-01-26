using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for CoverflowView.xaml
    /// </summary>
    public partial class CoverflowView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoverflowView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public CoverflowView(CoverflowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
