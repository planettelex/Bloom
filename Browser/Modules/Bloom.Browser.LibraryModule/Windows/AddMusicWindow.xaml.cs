using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using Bloom.Browser.LibraryModule.WindowModels;
using Bloom.Browser.PubSubEvents;
using Bloom.Browser.PubSubEvents.EventModels;
using Bloom.Domain.Enums;
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
            windowModel.BrowseFoldersCommand = new DelegateCommand<object>(BrowseFolders, CanBrowseFolders);
            windowModel.CancelCommand = new DelegateCommand<object>(Cancel, CanCancel);
            DataContext = windowModel;
        }
        private readonly FolderBrowserDialog _folderBrowserDialog;

        /// <summary>
        /// Gets the window model.
        /// </summary>
        private AddMusicWindowModel WindowModel { get { return (AddMusicWindowModel) DataContext; } }

        /// <summary>
        /// When overridden in a derived class, participates in rendering operations that are directed by the layout system. The rendering instructions for this element are not used directly when this method is invoked, and are instead preserved for later asynchronous use by layout and drawing.
        /// </summary>
        /// <param name="drawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (WindowModel.State.Connections.Count == 1)
                Libraries.SelectAll();
            
            WindowModel.IsLoading = false;
        }

        /// <summary>
        /// Determines whether this instance can browser folders.
        /// </summary>
        /// <param name="nothing">Nothing.</param>
        private bool CanBrowseFolders(object nothing)
        {
            return true;
        }

        /// <summary>
        /// Opens a folder browser dialogue.
        /// </summary>
        /// <param name="nothing">Nothing.</param>
        private void BrowseFolders(object nothing)
        {
            _folderBrowserDialog.SelectedPath = WindowModel.FolderPath;
            _folderBrowserDialog.ShowNewFolderButton = true;
            _folderBrowserDialog.Description = "Select a folder containing music files.";

            var result = _folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
                WindowModel.FolderPath = _folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// Determines whether this instance can be cancelled.
        /// </summary>
        /// <param name="nothing">Nothing.</param>
        private bool CanCancel(object nothing)
        {
            return true;
        }

        /// <summary>
        /// Closes the window without taking any action.
        /// </summary>
        /// <param name="nothing">Nothing.</param>
        private void Cancel(object nothing)
        {
            Close();
        }

        /// <summary>
        /// Determines whether this instance can add music.
        /// </summary>
        /// <param name="nothing">Nothing.</param>
        private bool CanAddMusic(object nothing)
        {
            return true;
        }

        /// <summary>
        /// Triggers a new add music tab event.
        /// </summary>
        /// <param name="nothing">Nothing.</param>
        private void AddMusic(object nothing)
        {
            if (WindowModel == null)
                return;

            var selectedSource = MusicSource.SelectedItem as ComboBoxItem;
            if (selectedSource == null)
                return;

            var addMusicEventModel = new AddMusicEventModel
            {
                Source = (MusicSource) Enum.Parse(typeof(MusicSource), selectedSource.Name),
                Path = WindowModel.FolderPath,
                CopyFiles = WindowModel.CopyFiles,
                LibraryIds = WindowModel.LibraryIds
            };

             WindowModel.EventAggregator.GetEvent<NewAddMusicTabEvent>().Publish(addMusicEventModel);
             Close();
        }

        /// <summary>
        /// Fires when the source combobox has been changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Fires when a library selection has been changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
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
