using Bloom.Analytics.Modules.HomeModule.ViewModels;

namespace Bloom.Analytics.Modules.HomeModule.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public HomeView(HomeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
