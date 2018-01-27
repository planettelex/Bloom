using Bloom.Browser.Modules.LibraryModule.ViewModels;

namespace Bloom.Browser.Modules.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for SpinesView.xaml
    /// </summary>
    public partial class SpinesView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpinesView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public SpinesView(SpinesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
