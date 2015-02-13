using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView
    {
        public ListView(ListViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
