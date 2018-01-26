using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for AlbumView.xaml
    /// </summary>
    public partial class AlbumView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public AlbumView(AlbumViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
