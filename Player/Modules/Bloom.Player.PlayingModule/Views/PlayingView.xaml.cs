using Bloom.Player.PlayingModule.ViewModels;

namespace Bloom.Player.PlayingModule.Views
{
    /// <summary>
    /// Interaction logic for PlayingView.xaml
    /// </summary>
    public partial class PlayingView
    {
        public PlayingView(PlayingViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
