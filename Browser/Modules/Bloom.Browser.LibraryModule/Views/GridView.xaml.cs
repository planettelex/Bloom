using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for GridView.xaml
    /// </summary>
    public partial class GridView
    {
        public GridView(GridViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
