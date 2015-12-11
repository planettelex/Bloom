using Bloom.Analytics.HomeModule.ViewModels;

namespace Bloom.Analytics.HomeModule.Views
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
