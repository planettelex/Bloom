using Bloom.Player.Modules.RecentModule.ViewModels;

namespace Bloom.Player.Modules.RecentModule.Views
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
