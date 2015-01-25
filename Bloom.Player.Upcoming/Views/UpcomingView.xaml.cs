using Bloom.Player.Upcoming.ViewModels;

namespace Bloom.Player.Upcoming.Views
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
