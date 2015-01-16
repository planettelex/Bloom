using System.Windows;
using Bloom.Services;

namespace Bloom.Browser
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        /// <param name="skinningService">The skinning service.</param>
        public Shell(ISkinningService skinningService)
        {
            InitializeComponent();
            var state = new State();
            DataContext = state;

            skinningService.SetSkin(state.Skin);
        }
    }
}
