using Bloom.Player.Recent.ViewModels;

namespace Bloom.Player.Recent.Views
{
    /// <summary>
    /// Interaction logic for RecentView.xaml
    /// </summary>
    public partial class RecentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecentView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public RecentView(RecentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
