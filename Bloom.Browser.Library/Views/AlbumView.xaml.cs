using Bloom.Browser.Library.ViewModels;

namespace Bloom.Browser.Library.Views
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
