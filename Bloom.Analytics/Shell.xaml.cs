using Bloom.Analytics.Common;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Bloom.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace Bloom.Analytics
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shell" /> class.
        /// </summary>
        /// <param name="skinningService">The skinning service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public Shell(ISkinningService skinningService, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            var state = new State();
            DataContext = state;

            skinningService.SetSkin(state.Skin);

            eventAggregator.GetEvent<AddTabEvent>().Subscribe(AddTab);
        }

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

        private void AddTab(Tab tab)
        {
            var newPane = new RadPane
            {
                Header = tab.Header,
                Content = tab.Content
            };

            PaneGroup.Items.Add(newPane);
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
