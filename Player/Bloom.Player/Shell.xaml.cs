﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using Bloom.Common;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Bloom.State.Services;

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
        /// <param name="stateService">The state service.</param>
        public Shell(ISkinningService skinningService, IStateService stateService)
        {
            InitializeComponent();
            _gridLengthConverter = new GridLengthConverter();
            _stateService = stateService;
            var state = _stateService.InitializeState(ProcessType.Player);
            DataContext = state;

            // Don't open in a minimized state.
            if (state.Player.WindowState == WindowState.Minimized)
                state.Player.WindowState = WindowState.Normal;

            WindowState = state.Player.WindowState;
            TitleBar.SetButtonVisibilties();
            SetRecentColumnWidth();
            SetUpcomingColumnWidth();
            skinningService.SetSkin(state.Player.SkinName);
        }
        private readonly IStateService _stateService;
        private readonly GridLengthConverter _gridLengthConverter;
        private BloomState State { get { return (BloomState) DataContext; } }

        #region Window Events

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Activated" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            // TODO: Check state database for new messages.
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            State.Player.WindowState = WindowState;
            _stateService.SaveState();
        }

        #endregion

        #region Grid Events

        private void OnRecentSizeChanged(object sender, SizeChangedEventArgs e)
        {
            State.Player.RecentWidth = Convert.ToInt32(RecentColumn.ActualWidth);
        }

        private void OnRecentSplitterMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            State.Player.ResetRecentWidth();
            SetRecentColumnWidth();
        }

        private void SetRecentColumnWidth()
        {
            var recentWidth = _gridLengthConverter.ConvertFromString(State.Player.RecentWidth.ToString(CultureInfo.InvariantCulture));
            if (recentWidth != null)
                RecentColumn.Width = (GridLength) recentWidth;
        }

        private void OnUpcomingSizeChanged(object sender, SizeChangedEventArgs e)
        {
            State.Player.UpcomingWidth = Convert.ToInt32(UpcomingColumn.ActualWidth);
        }

        private void OnUpcomingMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            State.Player.ResetUpcomingWidth();
            SetUpcomingColumnWidth();
        }

        private void SetUpcomingColumnWidth()
        {
            var upcomingWidth = _gridLengthConverter.ConvertFromString(State.Player.UpcomingWidth.ToString(CultureInfo.InvariantCulture));
            if (upcomingWidth != null)
                UpcomingColumn.Width = (GridLength) upcomingWidth;
        }

        #endregion
    }
}
