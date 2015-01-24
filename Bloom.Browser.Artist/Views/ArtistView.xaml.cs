using Bloom.Browser.Artist.ViewModels;

namespace Bloom.Browser.Artist.Views
{
    /// <summary>
    /// Interaction logic for ArtistView.xaml
    /// </summary>
    public partial class ArtistView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public ArtistView(ArtistViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
