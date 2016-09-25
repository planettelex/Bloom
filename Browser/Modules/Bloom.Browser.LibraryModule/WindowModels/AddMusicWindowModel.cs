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

namespace Bloom.Browser.LibraryModule.WindowModels
{
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
            State = (BrowserState)regionManager.Regions[Bloom.Common.Settings.MenuRegion].Context;
            FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            FolderSelectVisibility = Visibility.Collapsed;
            CopyFilesVisibility = Visibility.Collapsed;
            LibraryIds = new List<Guid>();
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

        public bool CopyFiles
        {
            get { return _copyFiles; }
            set { SetProperty(ref _copyFiles, value); }
        }
        private bool _copyFiles;

        public string FolderPath
        {
            get { return _folderPath; }
            set { SetProperty(ref _folderPath, value); }
        }
        private string _folderPath;

        public Visibility FolderSelectVisibility
        {
            get { return _folderSelectVisibility; }
            set { SetProperty(ref _folderSelectVisibility, value); }
        }
        private Visibility _folderSelectVisibility;

        public Visibility CopyFilesVisibility
        {
            get { return _copyFilesVisibility; }
            set { SetProperty(ref _copyFilesVisibility, value); }
        }
        private Visibility _copyFilesVisibility;

        public ICommand BrowseFoldersCommand { get; set; }

        public ICommand AddMusicCommand { get; set; }

        public ICommand CancelCommand { get; set; }
        
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

        public bool EvaluateValidity()
        {
            return !string.IsNullOrEmpty(FolderPath) &&
                      Directory.Exists(FolderPath) &&
                      LibraryIds.Count > 0;
        }

        public string Error { get { return null; } }
    }
}
