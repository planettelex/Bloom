using Bloom.Browser.Playlist.ViewModels;

namespace Bloom.Browser.Playlist.Views
{
    /// <summary>
    /// Interaction logic for PlaylistView.xaml
    /// </summary>
    public partial class PlaylistView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public PlaylistView(PlaylistViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
