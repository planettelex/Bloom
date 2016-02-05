using System;
using Bloom.Data.Interfaces;
using Bloom.LibraryModule.Services;
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
        /// <param name="suiteStateRepository">The suite state repository.</param>
        /// <param name="browserStateRepository">The browser state repository.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="tabRepository">The tab repository.</param>
        /// <param name="sharedLibraryService">The shared library service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public BrowserStateService(IDataSource stateDataSource, ISuiteStateRepository suiteStateRepository, IBrowserStateRepository browserStateRepository, 
            ILibraryConnectionRepository libraryConnectionRepository, ITabRepository tabRepository, ISharedLibraryService sharedLibraryService, IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            StateDataSource = stateDataSource;
            SuiteStateRepository = suiteStateRepository;
            LibraryConnectionRepository = libraryConnectionRepository;
            TabRepository = tabRepository;
            _sharedLibraryService = sharedLibraryService;
            _browserStateRepository = browserStateRepository;
            
            EventAggregator.GetEvent<SaveStateEvent>().Subscribe(SaveState);
            EventAggregator.GetEvent<ConnectionRemovedEvent>().Subscribe(CloseLibraryTabs);
        }
        private readonly IBrowserStateRepository _browserStateRepository;
        private readonly ISharedLibraryService _sharedLibraryService;

        /// <summary>
        /// Initializes the browser application state.
        /// </summary>
        /// <param name="user">The user.</param>
        public BrowserState InitializeState(User user)
        {
            State = _browserStateRepository.GetBrowserState(user) ?? NewBrowserState(user);
            SuiteState = SuiteStateRepository.GetSuiteState() ?? NewSuiteState();
            SuiteState.LastProcessAccess = ((BrowserState) State).ProcessName;
            SuiteState.ProcessAccessedOn = DateTime.Now;

            if (State.User == null) 
                return (BrowserState) State;

            State.User.LastLogin = DateTime.Now;
            if (State.Connections == null || State.Connections.Count <= 0)
                return (BrowserState) State;

            _sharedLibraryService.ConnectLibraries(State.Connections, user, false, true);
            SaveState();

            return (BrowserState) State;
        }

        private SuiteState NewSuiteState()
        {
            var suiteState = SuiteState.Create();
            SuiteStateRepository.AddSuiteState(suiteState);

            return suiteState;
        }

        private BrowserState NewBrowserState(User user)
        {
            var browserState = new BrowserState();
            browserState.SetUser(user);
            browserState.Connections = LibraryConnectionRepository.ListLibraryConnections(true);
            _browserStateRepository.AddBrowserState(browserState);

            return browserState;
        }

        private void SaveState(object nothing)
        {
            SaveState();
        }
    }
}
