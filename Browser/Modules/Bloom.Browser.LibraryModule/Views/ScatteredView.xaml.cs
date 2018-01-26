using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for ScatteredView.xaml
    /// </summary>
    public partial class ScatteredView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScatteredView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public ScatteredView(ScatteredViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
