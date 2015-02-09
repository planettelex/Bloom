using Bloom.Analytics.Library.ViewModels;

namespace Bloom.Analytics.Library.Views
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
