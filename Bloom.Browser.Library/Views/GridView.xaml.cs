using Bloom.Browser.Library.ViewModels;

namespace Bloom.Browser.Library.Views
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
