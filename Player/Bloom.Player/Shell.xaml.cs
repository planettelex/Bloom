using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using Bloom.Common;
using Bloom.Player.State.Services;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Player
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
        /// <param name="userService">The user service.</param>
        /// <param name="sharedLibraryService">The shared library service.</param>
        /// <param name="stateService">The state service.</param>
        public Shell(ISkinningService skinningService, IEventAggregator eventAggregator, IUserService userService, ISharedLibraryService sharedLibraryService, IPlayerStateService stateService)
        {
            InitializeComponent();
            _loading = true;
            _eventAggregator = eventAggregator;
            _gridLengthConverter = new GridLengthConverter();
            _sharedLibraryService = sharedLibraryService;
            _stateService = stateService;
            _stateService.ConnectDataSource();
            var user = userService.InitializeUser();
            var state = _stateService.InitializeState(user);
            DataContext = state;

            // Don't open in a minimized state.
            if (state.WindowState == WindowState.Minimized)
                state.WindowState = WindowState.Normal;

            WindowState = state.WindowState;
            TitleBar.SetButtonVisibilties();
            skinningService.SetSkin(state.SkinName);
            SetRecentColumnWidth();
            SetUpcomingColumnWidth();
        }
        private readonly IPlayerStateService _stateService;
        private readonly ISharedLibraryService _sharedLibraryService;
        private readonly GridLengthConverter _gridLengthConverter;
        private readonly IEventAggregator _eventAggregator;
        private bool _loading;

        private PlayerState State { get { return (PlayerState) DataContext; } }

        #region Window Events

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.ContentRendered" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            _loading = false;
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Publish(null);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Activated" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (!_loading)
            {
                var lastProcessToAccessState = _stateService.LastProcessToAccessState();
                var processChanged = lastProcessToAccessState != ProcessType.Player && lastProcessToAccessState != ProcessType.None;
                if (processChanged)
                {
                    _stateService.ChangeStateProcess(ProcessType.Player);
                    _sharedLibraryService.CheckLibraryConnections();
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            State.WindowState = WindowState;
            _stateService.SaveState();
        }

        #endregion

        #region Grid Events

        private void OnRecentSizeChanged(object sender, SizeChangedEventArgs e)
        {
            State.RecentWidth = Convert.ToInt32(RecentColumn.ActualWidth);
        }

        private void OnRecentSplitterMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            State.ResetRecentWidth();
            SetRecentColumnWidth();
        }

        private void SetRecentColumnWidth()
        {
            var recentWidth = _gridLengthConverter.ConvertFromString(State.RecentWidth.ToString(CultureInfo.InvariantCulture));
            if (recentWidth != null)
                RecentColumn.Width = (GridLength) recentWidth;
        }

        private void OnUpcomingSizeChanged(object sender, SizeChangedEventArgs e)
        {
            State.UpcomingWidth = Convert.ToInt32(UpcomingColumn.ActualWidth);
        }

        private void OnUpcomingMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            State.ResetUpcomingWidth();
            SetUpcomingColumnWidth();
        }

        private void SetUpcomingColumnWidth()
        {
            var upcomingWidth = _gridLengthConverter.ConvertFromString(State.UpcomingWidth.ToString(CultureInfo.InvariantCulture));
            if (upcomingWidth != null)
                UpcomingColumn.Width = (GridLength) upcomingWidth;
        }

        #endregion
    }
}
