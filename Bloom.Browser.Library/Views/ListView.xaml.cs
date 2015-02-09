using Bloom.Browser.Library.ViewModels;

namespace Bloom.Browser.Library.Views
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
