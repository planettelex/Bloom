using System;
using System.Collections.Generic;
using Bloom.Browser.Common;
using Bloom.Browser.Modules.LibraryModule.Services;
using Bloom.Browser.State.Domain.Models;
using Bloom.Domain.Enums;
using Bloom.PubSubEvents.EventModels;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.Modules.LibraryModule.ViewModels
{
    /// <summary>
    /// View model for NewMusicView.xaml.
    /// </summary>
    public class NewMusicViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewMusicViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="state">The state.</param>
        /// <param name="tabId">The tab identifier.</param>
        /// <param name="viewType">Type of the view.</param>
        /// <param name="addMusicEventModel">The add music event model.</param>
        /// <param name="importService">The import service.</param>
        public NewMusicViewModel(IEventAggregator eventAggregator, BrowserState state, Guid tabId, ViewType viewType, 
            AddMusicEventModel addMusicEventModel, IImportService importService)
        {
            EventAggregator = eventAggregator;
            _importService = importService;
            State = state;
            TabId = tabId;
            ViewType = viewType;
            if (addMusicEventModel == null)
                return;

            LibraryIds = addMusicEventModel.LibraryIds;
            Source = addMusicEventModel.Source;
            Path = addMusicEventModel.Path;
            CopyFiles = addMusicEventModel.CopyFiles;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewMusicViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="state">The state.</param>
        /// <param name="tabId">The tab identifier.</param>
        /// <param name="viewType">Type of the view.</param>
        /// <param name="libraryIds">The library ids.</param>
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

            var preferences = ImportPreferences.Create();

            switch (Source)
            {
                case MusicSource.Files:
                    _importService.ImportFiles(Path, LibraryIds, CopyFiles, preferences);
                    break;
            }
        }
    }
}
