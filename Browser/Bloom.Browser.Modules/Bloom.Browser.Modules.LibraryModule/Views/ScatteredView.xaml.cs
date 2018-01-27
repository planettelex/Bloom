using Bloom.Browser.Modules.LibraryModule.ViewModels;

namespace Bloom.Browser.Modules.LibraryModule.Views
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
