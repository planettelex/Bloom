using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for TilesView.xaml
    /// </summary>
    public partial class TilesView
    {
        public TilesView(TilesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
