using System;
using System.Collections.Generic;
using Bloom.Browser.Common;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.LibraryModule.ViewModels
{
    public class NewMusicViewModel
    {
        public NewMusicViewModel(IEventAggregator eventAggregator, BrowserState state, Guid tabId, ViewType viewType, List<Library> libraries, 
            MusicSource source, string path, bool copyFiles)
        {
            EventAggregator = eventAggregator;
            State = state;
            TabId = tabId;
            ViewType = viewType;
            Libraries = libraries;
            Source = source;
            Path = path;
            CopyFiles = copyFiles;
        }

        public NewMusicViewModel(IEventAggregator eventAggregator, BrowserState state, Guid tabId, ViewType viewType, List<Library> libraries)
        {
            EventAggregator = eventAggregator;
            State = state;
            TabId = tabId;
            ViewType = viewType;
            Libraries = libraries;
        }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        public IEventAggregator EventAggregator { get; private set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public BrowserState State { get; private set; }
        
        public Guid TabId { get; set; }

        public ViewType ViewType { get; set; }

        public List<Library> Libraries { get; set; }

        public MusicSource Source { get; set; }

        public string Path { get; set; }

        public bool CopyFiles { get; set; }
    }
}
