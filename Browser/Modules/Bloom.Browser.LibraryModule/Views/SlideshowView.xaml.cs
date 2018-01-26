using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for SlideshowView.xaml
    /// </summary>
    public partial class SlideshowView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlideshowView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public SlideshowView(SlideshowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
