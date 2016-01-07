using System;
using System.Windows.Media;
using Bloom.LibraryModule.WindowModels;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Telerik.Windows.Controls;

namespace Bloom.LibraryModule.Windows
{
    /// <summary>
    /// Interaction logic for ConnectedLibrariesWindow.xaml
    /// </summary>
    public partial class ConnectedLibrariesWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectedLibrariesWindow"/> class.
        /// </summary>
        /// <param name="windowModel">The window model.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="libraryService">The library service.</param>
        public ConnectedLibrariesWindow(ConnectedLibrariesWindowModel windowModel, IEventAggregator eventAggregator, ILibraryService libraryService)
        {
            InitializeComponent();
            _eventAggregator = eventAggregator;
            _libraryService = libraryService;
            windowModel.CloseCommand = new DelegateCommand<object>(Close, CanClose);
            windowModel.ConnectLibraryCommand = new DelegateCommand<Guid?>(ConnectLibrary, CanConnectLibrary);
            windowModel.DisconnectLibraryCommand = new DelegateCommand<Guid?>(DisconnectLibrary, CanDisconnectLibrary);

            DataContext = windowModel;
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly ILibraryService _libraryService;
        private LibraryConnection _disconnectingConnection;

        protected ApplicationState State
        {
            get { return ((ConnectedLibrariesWindowModel) DataContext).State; }
        }

        private bool CanConnectLibrary(Guid? libraryId)
        {
            return State != null;
        }

        private void ConnectLibrary(Guid? libraryId)
        {
            if (libraryId == null)
                return;

            var connectedLibrary = _libraryService.GetLibraryConnection(libraryId.Value, State.User, true);
            connectedLibrary.IsConnected = true;
            connectedLibrary.SetButtonVisibilities();
            connectedLibrary.BackgroundBrush = (Brush) FindResource("ControlBackgroundBrush");
            State.AddConnection(connectedLibrary);

            _eventAggregator.GetEvent<ConnectionAddedEvent>().Publish(connectedLibrary);
            _eventAggregator.GetEvent<SaveStateEvent>().Publish(null);
        }

        private bool CanDisconnectLibrary(Guid? libraryId)
        {
            return State != null;
        }

        private void DisconnectLibrary(Guid? libraryId)
        {
            if (libraryId == null)
                return;

            _disconnectingConnection = State.GetConnection(libraryId.Value);
            var tabbedApplicationState = State as TabbedApplicationState;
            if (tabbedApplicationState != null && tabbedApplicationState.HasLibraryTabs(libraryId.Value))
            {
                var confirmText = "Are you sure you would like to disconnect the library " + _disconnectingConnection.LibraryName + "?\r\n";
                confirmText += "If you do all tabs associated with this library will be closed.";
                Confirm(confirmText, OnDisconnectConfirmClosed);
            }
            else
            {
                DisconnectLibrary();
                _disconnectingConnection = null;
            }
        }

        private void DisconnectLibrary()
        {
            _disconnectingConnection.DataSource.Disconnect();
            _disconnectingConnection.IsConnected = false;
            _disconnectingConnection.SetButtonVisibilities();
            _disconnectingConnection.BackgroundBrush = (Brush)FindResource("ControlDisabledBackgroundBrush");
            State.RemoveConnection(_disconnectingConnection);

            _eventAggregator.GetEvent<ConnectionRemovedEvent>().Publish(_disconnectingConnection.LibraryId);
            _eventAggregator.GetEvent<SaveStateEvent>().Publish(null);
        }

        private void OnDisconnectConfirmClosed(object sender, WindowClosedEventArgs e)
        {
           if (e.DialogResult != null && e.DialogResult.Value)
               DisconnectLibrary();

            _disconnectingConnection = null;
        }

        private bool CanClose(object nothing)
        {
            return true;
        }

        private void Close(object nothing)
        {
            Close();
        }
    }
}
