using System;
using System.IO;
using System.Linq;
using System.Windows.Media;
using Bloom.Modules.LibraryModule.Services;
using Bloom.Modules.LibraryModule.WindowModels;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Prism.Commands;
using Microsoft.Win32;
using Telerik.Windows.Controls;

namespace Bloom.Modules.LibraryModule.Windows
{
    /// <summary>
    /// Interaction logic for ConnectedLibrariesWindow.xaml
    /// </summary>
    public partial class ConnectedLibrariesWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectedLibrariesWindow" /> class.
        /// </summary>
        /// <param name="windowModel">The window model.</param>
        /// <param name="sharedLibraryService">The shared library service.</param>
        public ConnectedLibrariesWindow(ConnectedLibrariesWindowModel windowModel, ISharedLibraryService sharedLibraryService)
        {
            InitializeComponent();
            _findFileDialog = new OpenFileDialog { Multiselect = false, DefaultExt = Common.Settings.LibraryFileExtension };
            _sharedLibraryService = sharedLibraryService;
            _enabledBrush = (Brush) FindResource("ControlBackgroundBrush");
            _disabledBrush = (Brush) FindResource("ControlDisabledBackgroundBrush");
            windowModel.CloseCommand = new DelegateCommand<object>(CloseWindow, CanCloseWindow);
            windowModel.ConnectNewLibraryCommand = new DelegateCommand<object>(ConnectNewLibrary, CanConnectNewLibrary);
            windowModel.ConnectLibraryCommand = new DelegateCommand<Guid?>(ConnectLibrary, CanConnectLibrary);
            windowModel.DisconnectLibraryCommand = new DelegateCommand<Guid?>(DisconnectLibrary, CanDisconnectLibrary);
            windowModel.FindLibraryFileCommand = new DelegateCommand<Guid?>(FindLibrary, CanFindLibrary);
            windowModel.RemoveConnectionCommand = new DelegateCommand<Guid?>(RemoveConnection, CanRemoveConnection);

            DataContext = windowModel;
            InitializeBackgroundBrushes();
        }
        private readonly OpenFileDialog _findFileDialog;
        private readonly ISharedLibraryService _sharedLibraryService;
        private LibraryConnection _disconnectingConnection;
        private LibraryConnection _removingConnection;
        private readonly Brush _enabledBrush;
        private readonly Brush _disabledBrush;

        /// <summary>
        /// Gets the window model.
        /// </summary>
        protected ConnectedLibrariesWindowModel WindowModel => (ConnectedLibrariesWindowModel) DataContext;

        /// <summary>
        /// Initializes the background brushes.
        /// </summary>
        private void InitializeBackgroundBrushes()
        {
            foreach (var connection in WindowModel.LibraryConnections)
            {
                if (!connection.IsConnected || !File.Exists(connection.FilePath))
                    connection.BackgroundBrush = _disabledBrush;
                else
                    connection.BackgroundBrush = _enabledBrush;
            }
        }

        /// <summary>
        /// Determines whether this window can use the connect library command.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        private bool CanConnectLibrary(Guid? libraryId)
        {
            return WindowModel.State != null;
        }

        /// <summary>
        /// The connect library command.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        private void ConnectLibrary(Guid? libraryId)
        {
            if (libraryId == null)
                return;

            var libraryToConnect = WindowModel.LibraryConnections.SingleOrDefault(library => library.LibraryId == libraryId.Value);
            if (libraryToConnect == null)
                return;

            libraryToConnect.IsConnected = _sharedLibraryService.ConnectLibrary(libraryToConnect, WindowModel.State.User, true, true);
            libraryToConnect.SetButtonVisibilities();
            if (libraryToConnect.IsConnected)
            {
                libraryToConnect.BackgroundBrush = _enabledBrush;
                WindowModel.State.AddConnection(libraryToConnect);
                WindowModel.EventAggregator.GetEvent<ConnectionAddedEvent>().Publish(libraryToConnect);
                WindowModel.EventAggregator.GetEvent<SaveStateEvent>().Publish(WindowModel.State);
            }
            else
                libraryToConnect.BackgroundBrush = _disabledBrush;
        }

        /// <summary>
        /// Determines whether this window can use the disconnect library command.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        private bool CanDisconnectLibrary(Guid? libraryId)
        {
            return WindowModel.State != null;
        }

        /// <summary>
        /// The disconnect library command.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        private void DisconnectLibrary(Guid? libraryId)
        {
            if (libraryId == null)
                return;

            _disconnectingConnection = WindowModel.State.GetConnection(libraryId.Value);
            var tabbedApplicationState = WindowModel.State as TabbedApplicationState;
            if (tabbedApplicationState != null && tabbedApplicationState.HasLibraryTabs(libraryId.Value))
            {
                var confirmText = "Are you sure you would like to disconnect the library \"" + _disconnectingConnection.LibraryName + "\"?\r\n";
                confirmText += "If you do all tabs associated with this library will be closed.";
                Confirm(confirmText, DisconnectConfirmClosed);
            }
            else
            {
                DisconnectLibrary();
                _disconnectingConnection = null;
            }
        }

        /// <summary>
        /// Disconnects the library.
        /// </summary>
        private void DisconnectLibrary()
        {
            _disconnectingConnection.Disconnect();
            _disconnectingConnection.SetButtonVisibilities();
            _disconnectingConnection.BackgroundBrush = _disabledBrush;
            WindowModel.State.RemoveConnection(_disconnectingConnection);

            WindowModel.EventAggregator.GetEvent<ConnectionRemovedEvent>().Publish(_disconnectingConnection.LibraryId);
            WindowModel.EventAggregator.GetEvent<SaveStateEvent>().Publish(WindowModel.State);
        }

        /// <summary>
        /// Occurs when the disconnect confirm dialogue has closed.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">The <see cref="WindowClosedEventArgs"/> instance containing the event data.</param>
        private void DisconnectConfirmClosed(object sender, WindowClosedEventArgs e)
        {
           if (e.DialogResult != null && e.DialogResult.Value)
               DisconnectLibrary();

            _disconnectingConnection = null;
        }

        /// <summary>
        /// Determines whether this window can use the find library command.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        private bool CanFindLibrary(Guid? libraryId)
        {
            return WindowModel.State != null;
        }

        /// <summary>
        /// The find library command.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        /// <exception cref="System.InvalidOperationException">Library connection for libraryId.Value not found in the observable collection.</exception>
        private void FindLibrary(Guid? libraryId)
        {
            if (libraryId == null)
                return;

            var lostLibraryConnection = WindowModel.LibraryConnections.SingleOrDefault(library => library.LibraryId == libraryId.Value);
            
            if (lostLibraryConnection == null)
                throw new InvalidOperationException("Library connection for id " + libraryId.Value + " not found in the observable collection.");

            var brokenFilePath = lostLibraryConnection.FilePath;
            _findFileDialog.InitialDirectory = Path.GetDirectoryName(brokenFilePath) ?? brokenFilePath;
            _findFileDialog.FileName = Path.GetFileName(brokenFilePath) ?? brokenFilePath;
            _findFileDialog.Title = "Locate the *" + Common.Settings.LibraryFileExtension + " file for " + lostLibraryConnection.LibraryName;

            var result = _findFileDialog.ShowDialog();
            if (result != null && result.Value)
            {
                lostLibraryConnection.FilePath = _findFileDialog.FileName;
                _sharedLibraryService.ConnectLibrary(lostLibraryConnection, WindowModel.State.User, false, true);
                if (lostLibraryConnection.LibraryId != lostLibraryConnection.Library.Id)
                {
                    lostLibraryConnection.FilePath = brokenFilePath;
                    lostLibraryConnection.Disconnect();
                    var alertText = "The library file specfied is not for the library named: \"" + lostLibraryConnection.LibraryName + "\"\r\n";
                    alertText += "Please select the library file which used to be at:\r\n\r\n" + lostLibraryConnection.FilePath + "\r\n\r\n";
                    alertText += "It is OK if file name is different than before.";
                    Alert(alertText);
                }
                else
                {
                    _sharedLibraryService.SyncLibraryOwnerAndUser(lostLibraryConnection, WindowModel.State.User);
                    lostLibraryConnection.Disconnect();
                    lostLibraryConnection.SetButtonVisibilities();
                    WindowModel.EventAggregator.GetEvent<SaveStateEvent>().Publish(WindowModel.State);
                }
            }
        }

        /// <summary>
        /// Determines whether this window can use the remove connection command.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        private bool CanRemoveConnection(Guid? libraryId)
        {
            return WindowModel.State != null;
        }

        /// <summary>
        /// The remove connection command.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        private void RemoveConnection(Guid? libraryId)
        {
            if (libraryId == null)
                return;

            _removingConnection = WindowModel.LibraryConnections.SingleOrDefault(library => library.LibraryId == libraryId.Value);
            if (_removingConnection == null)
                return;

            var confirmText = "Are you sure you would like to permanently remove\r\nthe connection to the library: \"" + _removingConnection.LibraryName + "\"?";
            Confirm(confirmText, RemoveConnectionConfirmClosed);
        }

        /// <summary>
        /// Occurs when the remove connection confirm dialogue has closed.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">The <see cref="WindowClosedEventArgs"/> instance containing the event data.</param>
        private void RemoveConnectionConfirmClosed(object sender, WindowClosedEventArgs e)
        {
            if (e.DialogResult != null && e.DialogResult.Value)
            {
                WindowModel.LibraryConnections.Remove(_removingConnection);
                _sharedLibraryService.RemoveLibraryConnection(_removingConnection);
                WindowModel.EventAggregator.GetEvent<SaveStateEvent>().Publish(WindowModel.State);
            }

            _removingConnection = null;
        }

        /// <summary>
        /// Determines whether this window can use the close window command.
        /// </summary>
        private bool CanCloseWindow(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The close window command.
        /// </summary>
        private void CloseWindow(object nothing)
        {
            Close();
        }

        /// <summary>
        /// Determines whether this window can use the connect new library command.
        /// </summary>
        private bool CanConnectNewLibrary(object nothing)
        {
            return true;
        }

        /// <summary>
        /// The connect new library command.
        /// </summary>
        private void ConnectNewLibrary(object nothing)
        {
            _findFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            _findFileDialog.FileName = string.Empty;
            _findFileDialog.Title = "Locate the *" + Common.Settings.LibraryFileExtension + " file for the library to connect.";

            var result = _findFileDialog.ShowDialog();
            if (result != null && result.Value)
            {
                var libraryConnection = _sharedLibraryService.ConnectNewLibrary(_findFileDialog.FileName);
                if (libraryConnection == null)
                    return;

                libraryConnection.SetButtonVisibilities();
                libraryConnection.BackgroundBrush = libraryConnection.IsConnected ? _enabledBrush : _disabledBrush;
                var insertAt = 0;
                for (var i = 0; i < WindowModel.LibraryConnections.Count; i++)
                {
                    if (string.Compare(WindowModel.LibraryConnections[i].LibraryName, libraryConnection.LibraryName, StringComparison.Ordinal) > 0)
                    {
                        insertAt = i;
                        break;
                    }
                }
                if (!WindowModel.LibraryConnections.Contains(libraryConnection))
                    WindowModel.LibraryConnections.Insert(insertAt, libraryConnection);
            }
        }
    }
}
