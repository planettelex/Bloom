using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.Modules.LibraryModule.WindowModels
{
    /// <summary>
    /// The window model for the add music dialogue window.
    /// </summary>
    /// <seealso cref="Microsoft.Practices.Prism.Mvvm.BindableBase" />
    /// <seealso cref="System.ComponentModel.IDataErrorInfo" />
    public class AddMusicWindowModel : BindableBase, IDataErrorInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddMusicWindowModel"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public AddMusicWindowModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            State = (BrowserState) regionManager.Regions[Bloom.Common.Settings.MenuRegion].Context;
            LibraryIds = new List<Guid>();
            FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            FolderSelectVisibility = Visibility.Collapsed;
            CopyFilesVisibility = Visibility.Collapsed;
            CopyFiles = true;
        }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        public IEventAggregator EventAggregator { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        public bool IsLoading { get; set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public BrowserState State { get; private set; }

        /// <summary>
        /// Gets or sets the library identifiers.
        /// </summary>
        public List<Guid> LibraryIds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        public bool IsValid
        {
            get { return _isValid; }
            set { SetProperty(ref _isValid, value); }
        }
        private bool _isValid;

        /// <summary>
        /// Gets or sets a value indicating whether to copy the music files to the library folder.
        /// </summary>
        public bool CopyFiles
        {
            get { return _copyFiles; }
            set { SetProperty(ref _copyFiles, value); }
        }
        private bool _copyFiles;

        /// <summary>
        /// Gets or sets the folder path.
        /// </summary>
        public string FolderPath
        {
            get { return _folderPath; }
            set { SetProperty(ref _folderPath, value); }
        }
        private string _folderPath;

        /// <summary>
        /// Gets or sets the visibility of the folder select area.
        /// </summary>
        public Visibility FolderSelectVisibility
        {
            get { return _folderSelectVisibility; }
            set { SetProperty(ref _folderSelectVisibility, value); }
        }
        private Visibility _folderSelectVisibility;

        /// <summary>
        /// Gets or sets the visibility of the copy files checkbox.
        /// </summary>
        public Visibility CopyFilesVisibility
        {
            get { return _copyFilesVisibility; }
            set { SetProperty(ref _copyFilesVisibility, value); }
        }
        private Visibility _copyFilesVisibility;

        /// <summary>
        /// Gets or sets the browse folders command.
        /// </summary>
        public ICommand BrowseFoldersCommand { get; set; }

        /// <summary>
        /// Gets or sets the add music command.
        /// </summary>
        public ICommand AddMusicCommand { get; set; }

        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        public string this[string columnName]
        {
            get
            {
                IsValid = false;
                if (IsLoading)
                    return null;

                if (columnName == "FolderPath")
                {
                    if (string.IsNullOrEmpty(FolderPath))
                        return "Folder path is required";
                    if (!Directory.Exists(FolderPath))
                        return "Specified folder does not exist";
                }

                IsValid = EvaluateValidity();

                return null;
            }
        }

        /// <summary>
        /// Evaluates the validity of this window.
        /// </summary>
        public bool EvaluateValidity()
        {
            return !string.IsNullOrEmpty(FolderPath) &&
                      Directory.Exists(FolderPath) &&
                      LibraryIds.Count > 0;
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        public string Error => null;
    }
}
