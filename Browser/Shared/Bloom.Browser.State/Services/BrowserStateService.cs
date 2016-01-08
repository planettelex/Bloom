using System;
using Bloom.Data.Interfaces;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.State.Services
{
    /// <summary>
    /// Service for managing the browser application state.
    /// </summary>
    public class BrowserStateService : TabbedStateBaseService, IBrowserStateService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserStateService" /> class.
        /// </summary>
        /// <param name="stateDataSource">The state data source.</param>
        /// <param name="browserStateRepository">The browser state repository.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="tabRepository">The tab repository.</param>
        /// <param name="libraryService">The library service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public BrowserStateService(IDataSource stateDataSource, IBrowserStateRepository browserStateRepository, ILibraryConnectionRepository libraryConnectionRepository, ITabRepository tabRepository, ILibraryService libraryService, IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            StateDataSource = stateDataSource;
            TabRepository = tabRepository;
            _libraryConnectionRepository = libraryConnectionRepository;
            _browserStateRepository = browserStateRepository;
            _libraryService = libraryService;

            EventAggregator.GetEvent<SaveStateEvent>().Subscribe(SaveState);
            EventAggregator.GetEvent<ConnectionRemovedEvent>().Subscribe(CloseLibraryTabs);
        }
        private readonly ILibraryService _libraryService;
        private readonly IBrowserStateRepository _browserStateRepository;
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;

        /// <summary>
        /// Initializes the browser application state.
        /// </summary>
        /// <param name="user">The user.</param>
        public BrowserState InitializeState(User user)
        { 
            State = _browserStateRepository.GetBrowserState(user) ?? AddNewState(user);

            if (State.User == null) 
                return (BrowserState) State;

            State.User.LastLogin = DateTime.Now;
            if (State.Connections == null || State.Connections.Count <= 0)
                return (BrowserState) State;

            _libraryService.ConnectLibraries(State.Connections, user, false, true);
            SaveState();

            return (BrowserState) State;
        }

        private BrowserState AddNewState(User user)
        {
            var browserState = new BrowserState();
            browserState.SetUser(user);
            browserState.Connections = _libraryConnectionRepository.ListLibraryConnections(true);
            _browserStateRepository.AddBrowserState(browserState);

            EventAggregator.GetEvent<UserChangedEvent>().Publish(null);
            return browserState;
        }

        private void SaveState(object nothing)
        {
            SaveState();
        }
    }
}
