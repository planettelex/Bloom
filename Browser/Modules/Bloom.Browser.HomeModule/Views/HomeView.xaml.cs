using Bloom.Browser.HomeModule.ViewModels;

namespace Bloom.Browser.HomeModule.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView
    {
        public HomeView(HomeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
