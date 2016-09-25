using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using Bloom.Browser.LibraryModule.WindowModels;
using Bloom.Browser.PubSubEvents;
using Bloom.Browser.PubSubEvents.EventModels;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Commands;

namespace Bloom.Browser.LibraryModule.Windows
{
    /// <summary>
    /// Interaction logic for AddMusicWindow.xaml
    /// </summary>
    public partial class AddMusicWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddMusicWindow"/> class.
        /// </summary>
        /// <param name="windowModel">The window model.</param>
        public AddMusicWindow(AddMusicWindowModel windowModel)
        {
            InitializeComponent();
            _folderBrowserDialog = new FolderBrowserDialog();
            windowModel.IsLoading = true;
            windowModel.AddMusicCommand = new DelegateCommand<object>(AddMusic, CanAddMusic);
            windowModel.BrowseFoldersCommand = new DelegateCommand<object>(BrowserFolders, CanBrowserFolders);
            windowModel.CancelCommand = new DelegateCommand<object>(Cancel, CanCancel);
            DataContext = windowModel;
        }
        private readonly FolderBrowserDialog _folderBrowserDialog;

        private AddMusicWindowModel WindowModel { get { return (AddMusicWindowModel) DataContext; } }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (WindowModel.State.Connections.Count == 1)
                Libraries.SelectAll();
            
            WindowModel.IsLoading = false;
        }

        private bool CanBrowserFolders(object nothing)
        {
            return true;
        }

        private void BrowserFolders(object nothing)
        {
            _folderBrowserDialog.SelectedPath = WindowModel.FolderPath;
            _folderBrowserDialog.ShowNewFolderButton = true;
            _folderBrowserDialog.Description = "Select a folder containing music files.";

            var result = _folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
                WindowModel.FolderPath = _folderBrowserDialog.SelectedPath;
        }

        private bool CanCancel(object nothing)
        {
            return true;
        }

        private void Cancel(object nothing)
        {
            Close();
        }

        private bool CanAddMusic(object nothing)
        {
            return true;
        }

        private void AddMusic(object nothing)
        {
            if (WindowModel == null)
                return;

            var selectedSource = MusicSource.SelectedItem as ComboBoxItem;
            if (selectedSource == null)
                return;

            var addMusicEventModel = new AddMusicEventModel
            {
                Source = selectedSource.Name,
                FromPath = WindowModel.FolderPath,
                CopyFiles = WindowModel.CopyFiles,
                LibraryIds = WindowModel.LibraryIds
            };

             WindowModel.EventAggregator.GetEvent<NewAddMusicTabEvent>().Publish(addMusicEventModel);
        }

        private void SourceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WindowModel == null)
                return;

            var selectedSource = MusicSource.SelectedItem as ComboBoxItem;
            if (selectedSource == null)
                return;

            WindowModel.FolderSelectVisibility = selectedSource.Name == "Files" ? Visibility.Visible : Visibility.Collapsed;
            WindowModel.CopyFilesVisibility = selectedSource.Name == "Select" ? Visibility.Collapsed : Visibility.Visible;
            WindowModel.IsValid = selectedSource.Name != "Select" && WindowModel.EvaluateValidity();
        }

        private void LibrarySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
                WindowModel.LibraryIds.Add(((LibraryConnection) e.AddedItems[0]).LibraryId);

            if (e.RemovedItems != null && e.RemovedItems.Count > 0)
                WindowModel.LibraryIds.Remove(((LibraryConnection) e.RemovedItems[0]).LibraryId);

            var selectedSource = MusicSource.SelectedItem as ComboBoxItem;
            if (selectedSource == null)
                return;

            WindowModel.IsValid = selectedSource.Name != "Select" && WindowModel.EvaluateValidity();
        }
    }
}
