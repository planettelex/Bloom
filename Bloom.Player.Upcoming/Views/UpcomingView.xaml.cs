using Bloom.Player.UpcomingModule.ViewModels;

namespace Bloom.Player.UpcomingModule.Views
{
    /// <summary>
    /// Interaction logic for UpcomingView.xaml
    /// </summary>
    public partial class UpcomingView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpcomingView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public UpcomingView(UpcomingViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
