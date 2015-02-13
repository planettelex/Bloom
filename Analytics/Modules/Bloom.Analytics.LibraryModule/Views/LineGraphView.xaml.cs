using Bloom.Analytics.LibraryModule.ViewModels;

namespace Bloom.Analytics.LibraryModule.Views
{
    /// <summary>
    /// Interaction logic for LineGraphView.xaml
    /// </summary>
    public partial class LineGraphView
    {
        public LineGraphView(LineGraphViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
