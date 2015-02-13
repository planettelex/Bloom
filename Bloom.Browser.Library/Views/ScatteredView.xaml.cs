using Bloom.Browser.LibraryModule.ViewModels;

namespace Bloom.Browser.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for ScatteredView.xaml
    /// </summary>
    public partial class ScatteredView
    {
        public ScatteredView(ScatteredViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
