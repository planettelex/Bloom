using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for AlbumView.xaml
    /// </summary>
    public partial class AlbumView
    {
        public AlbumView(AlbumViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
