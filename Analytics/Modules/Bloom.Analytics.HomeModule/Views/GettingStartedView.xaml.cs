using Bloom.Analytics.HomeModule.ViewModels;

namespace Bloom.Analytics.HomeModule.Views
{
    /// <summary>
    /// Interaction logic for GettingStartedView.xaml
    /// </summary>
    public partial class GettingStartedView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GettingStartedView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public GettingStartedView(GettingStartedViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
