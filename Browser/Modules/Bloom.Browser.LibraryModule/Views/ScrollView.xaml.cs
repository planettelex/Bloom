using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for ScrollView.xaml
    /// </summary>
    public partial class ScrollView
    {
        public ScrollView(ScrollViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
