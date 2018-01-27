using Bloom.Player.Modules.VolumeModule.ViewModels;

namespace Bloom.Player.Modules.VolumeModule.Views
{
    /// <summary>
    /// Interaction logic for VolumeView.xaml
    /// </summary>
    public partial class VolumeView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VolumeView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public VolumeView(VolumeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
