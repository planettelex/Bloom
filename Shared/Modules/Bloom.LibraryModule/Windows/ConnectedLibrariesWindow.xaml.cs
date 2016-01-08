using System;
using System.IO;
using System.Linq;
using System.Windows.Media;
using Bloom.Common.ExtensionMethods;
using Bloom.LibraryModule.Services;
using Bloom.LibraryModule.WindowModels;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Win32;
using Telerik.Windows.Controls;

namespace Bloom.LibraryModule.Windows
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
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="libraryService">The library service.</param>
        /// <param name="sharedLibraryService">The shared library service.</param>
        public ConnectedLibrariesWindow(ConnectedLibrariesWindowModel windowModel, IEventAggregator eventAggregator, ILibraryService libraryService, ISharedLibraryService sharedLibraryService)
        {
            InitializeComponent();
            _findFileDialog = new OpenFileDialog { Multiselect = false, DefaultExt = Common.Settings.LibraryFileExtension };
            _eventAggregator = eventAggregator;
            _libraryService = libraryService;
            _sharedLibraryService = sharedLibraryService;
            _enabledBrush = (Brush) FindResource("ControlBackgroundBrush");
            _disabledBrush = (Brush) FindResource("ControlDisabledBackgroundBrush");
            windowModel.CloseCommand = new DelegateCommand<object>(Close, CanClose);
            windowModel.ConnectNewLibraryCommand = new DelegateCommand<object>(ConnectNewLibrary, CanConnectNewLibrary);
            windowModel.ConnectLibraryCommand = new DelegateCommand<Guid?>(ConnectLibrary, CanConnectLibrary);
            windowModel.DisconnectLibraryCommand = new DelegateCommand<Guid?>(DisconnectLibrary, CanDisconnectLibrary);
            windowModel.FindLibraryFileCommand = new DelegateCommand<Guid?>(FindLibrary, CanFindLibrary);
            windowModel.RemoveConnectionCommand = new DelegateCommand<Guid?>(RemoveConnection, CanRemoveConnection);

            DataContext = windowModel;
            InitializeBackgroundBrushes();
        }
        private readonly OpenFileDialog _findFileDialog;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILibraryService _libraryService;
        private readonly ISharedLibraryService _sharedLibraryService;
        private LibraryConnection _disconnectingConnection;
        private LibraryConnection _removingConnection;
        private readonly Brush _enabledBrush;
        private readonly Brush _disabledBrush;

        protected ConnectedLibrariesWindowModel Model
        {
            get { return (ConnectedLibrariesWindowModel) DataContext; }
        }

        private void InitializeBackgroundBrushes()
        {
            foreach (var connection in Model.LibraryConnections)
            {
                if (!connection.IsConnected || !File.Exists(connection.FilePath))
                    connection.BackgroundBrush = _disabledBrush;
                else
                    connection.BackgroundBrush = _enabledBrush;
            }
        }

        private bool CanConnectLibrary(Guid? libraryId)
        {
            return Model.State != null;
        }

        private void ConnectLibrary(Guid? libraryId)
        {
            if (libraryId == null)
                return;

            var library = _libraryService.GetLibraryConnection(libraryId.Value);
            library.IsConnected = _libraryService.ConnectLibrary(library, Model.State.User, true, true);
            library.SetButtonVisibilities();
            if (library.IsConnected)
            {
                library.BackgroundBrush = _enabledBrush;
                Model.State.AddConnection(library);
                _eventAggregator.GetEvent<ConnectionAddedEvent>().Publish(library);
                _eventAggregator.GetEvent<SaveStateEvent>().Publish(null);
            }
            else
                library.BackgroundBrush = _disabledBrush;
        }

        private bool CanDisconnectLibrary(Guid? libraryId)
        {
            return Model.State != null;
        }

        private void DisconnectLibrary(Guid? libraryId)
        {
            if (libraryId == null)
                return;

            _disconnectingConnection = Model.State.GetConnection(libraryId.Value);
            var tabbedApplicationState = Model.State as TabbedApplicationState;
            if (tabbedApplicationState != null && tabbedApplicationState.HasLibraryTabs(libraryId.Value))
            {
                var confirmText = "Are you sure you would like to disconnect the library \"" + _disconnectingConnection.LibraryName + "\"?\r\n";
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
            _disconnectingConnection.BackgroundBrush = _disabledBrush;
            Model.State.RemoveConnection(_disconnectingConnection);

            _eventAggregator.GetEvent<ConnectionRemovedEvent>().Publish(_disconnectingConnection.LibraryId);
            _eventAggregator.GetEvent<SaveStateEvent>().Publish(null);
        }

        private void OnDisconnectConfirmClosed(object sender, WindowClosedEventArgs e)
        {
           if (e.DialogResult != null && e.DialogResult.Value)
               DisconnectLibrary();

            _disconnectingConnection = null;
        }

        private bool CanFindLibrary(Guid? libraryId)
        {
            return Model.State != null;
        }

        private void FindLibrary(Guid? libraryId)
        {
            if (libraryId == null)
                return;

            var lostLibraryConnection = Model.LibraryConnections.SingleOrDefault(library => library.LibraryId == libraryId.Value);
            
            if (lostLibraryConnection == null)
                throw new InvalidOperationException("Library connection for id " + libraryId.Value + " not found in the observable collection.");

            var brokenFilePath = lostLibraryConnection.FilePath;
            _findFileDialog.InitialDirectory = brokenFilePath.GetFilePath();
            _findFileDialog.FileName = brokenFilePath.GetFileName();
            _findFileDialog.Title = "Locate the *" + Common.Settings.LibraryFileExtension + " file for " + lostLibraryConnection.LibraryName;

            var result = _findFileDialog.ShowDialog();
            if (result != null && result.Value)
            {
                lostLibraryConnection.FilePath = _findFileDialog.FileName;
                _libraryService.ConnectLibrary(lostLibraryConnection, Model.State.User, false, true);
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
                    _libraryService.SyncLibraryOwnerAndUser(lostLibraryConnection, Model.State.User);
                    lostLibraryConnection.Disconnect();
                    lostLibraryConnection.SetButtonVisibilities();
                    _eventAggregator.GetEvent<SaveStateEvent>().Publish(null);
                }
            }
        }

        private bool CanRemoveConnection(Guid? libraryId)
        {
            return Model.State != null;
        }

        private void RemoveConnection(Guid? libraryId)
        {
            if (libraryId == null)
                return;

            _removingConnection = Model.LibraryConnections.SingleOrDefault(library => library.LibraryId == libraryId.Value);
            if (_removingConnection == null)
                return;

            var confirmText = "Are you sure you would like to permanently remove\r\nthe connection to the library: \"" + _removingConnection.LibraryName + "\"?";
            Confirm(confirmText, OnRemoveConnectionConfirmClosed);
        }

        private void OnRemoveConnectionConfirmClosed(object sender, WindowClosedEventArgs e)
        {
            if (e.DialogResult != null && e.DialogResult.Value)
            {
                Model.LibraryConnections.Remove(_removingConnection);
                _libraryService.RemoveLibraryConnection(_removingConnection);
                _eventAggregator.GetEvent<SaveStateEvent>().Publish(null);
            }

            _removingConnection = null;
        }

        private bool CanClose(object nothing)
        {
            return true;
        }

        private void Close(object nothing)
        {
            Close();
        }

        private bool CanConnectNewLibrary(object nothing)
        {
            return true;
        }

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
                for (var i = 0; i < Model.LibraryConnections.Count; i++)
                {
                    if (string.Compare(Model.LibraryConnections[i].LibraryName, libraryConnection.LibraryName, StringComparison.Ordinal) > 0)
                    {
                        insertAt = i;
                        break;
                    }
                }
                if (!Model.LibraryConnections.Contains(libraryConnection))
                    Model.LibraryConnections.Insert(insertAt, libraryConnection);
            }
        }
    }
}
