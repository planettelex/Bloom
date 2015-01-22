using System.Windows.Input;
using Bloom.Browser.Common;
using Bloom.Services;
using Telerik.Windows.Controls.Docking;

namespace Bloom.Browser
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell
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

        /// <summary>
        /// The toggle window state command.
        /// </summary>
        public static RoutedCommand ToggleWindowStateCommand = new RoutedCommand();

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            var state = (State) DataContext;
            state.Save();
        }

        private void DockCompassPreview(object sender, PreviewShowCompassEventArgs e)
        {
            if (e.TargetGroup != null && e.TargetGroup.Name == "Sidebar")
            {
                e.Compass.IsLeftIndicatorVisible = false;
                e.Compass.IsTopIndicatorVisible = false;
                e.Compass.IsRightIndicatorVisible = false;
                e.Compass.IsBottomIndicatorVisible = false;
                e.Compass.IsCenterIndicatorVisible = false;
            }
            else
            {
                e.Compass.IsLeftIndicatorVisible = false;
                e.Compass.IsTopIndicatorVisible = false;
                e.Compass.IsRightIndicatorVisible = false;
                e.Compass.IsBottomIndicatorVisible = false;
                e.Compass.IsCenterIndicatorVisible = true;
            }
        }
    }
}
