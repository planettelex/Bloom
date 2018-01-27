using Bloom.Player.Modules.VisualsModule.ViewModels;

namespace Bloom.Player.Modules.VisualsModule.Views
{
    /// <summary>
    /// Interaction logic for VisualsView.xaml
    /// </summary>
    public partial class VisualsView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualsView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public VisualsView(VisualsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
