using System;
using System.Collections.Generic;
using Bloom.Browser.Common;
using Bloom.Browser.LibraryModule.Services;
using Bloom.Domain.Enums;
using Bloom.PubSubEvents.EventModels;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.LibraryModule.ViewModels
{
    /// <summary>
    /// View model for NewMusicView.xaml.
    /// </summary>
    public class NewMusicViewModel
    {
        public NewMusicViewModel(IEventAggregator eventAggregator, BrowserState state, Guid tabId, ViewType viewType, 
            AddMusicEventModel addMusicEventModel, IImportService importService)
        {
            EventAggregator = eventAggregator;
            _importService = importService;
            State = state;
            TabId = tabId;
            ViewType = viewType;
            if (addMusicEventModel != null)
            {
                LibraryIds = addMusicEventModel.LibraryIds;
                Source = addMusicEventModel.Source;
                Path = addMusicEventModel.Path;
                CopyFiles = addMusicEventModel.CopyFiles;
            }
        }

        public NewMusicViewModel(IEventAggregator eventAggregator, BrowserState state, Guid tabId, ViewType viewType, List<Guid> libraryIds)
        {
            EventAggregator = eventAggregator;
            State = state;
            TabId = tabId;
            ViewType = viewType;
            LibraryIds = libraryIds;
        }
        private readonly IImportService _importService;

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        public IEventAggregator EventAggregator { get; private set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public BrowserState State { get; private set; }

        /// <summary>
        /// Gets or sets the tab identifier.
        /// </summary>
        public Guid TabId { get; set; }

        /// <summary>
        /// Gets or sets the type of the view.
        /// </summary>
        public ViewType ViewType { get; set; }

        /// <summary>
        /// Gets or sets the library ids.
        /// </summary>
        public List<Guid> LibraryIds { get; set; }

        /// <summary>
        /// Gets or sets the music source.
        /// </summary>
        public MusicSource Source { get; set; }

        /// <summary>
        /// Gets or sets the path to the music.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to copy files to the library folder.
        /// </summary>
        public bool CopyFiles { get; set; }

        /// <summary>
        /// Starts a music import service.
        /// </summary>
        public void StartImport()
        {
            if (_importService == null || _importService.IsRunning())
                return;

            switch (Source)
            {
                case MusicSource.Files:
                    _importService.ImportFiles(Path, LibraryIds, CopyFiles);
                    break;
            }
        }
    }
}
