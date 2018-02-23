using System;
using Bloom.Data.Interfaces;
using Bloom.Modules.LibraryModule.Services;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Prism.Events;

namespace Bloom.Browser.State.Services
{
    /// <summary>
    /// Service for managing the browser application state.
    /// </summary>
    /// <seealso cref="Bloom.Services.TabbedStateBaseService" />
    /// <seealso cref="Bloom.Browser.State.Services.IBrowserStateService" />
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
            EventAggregator.GetEvent<UserChangedEvent>().Subscribe(RemoveAnonymousState);
            EventAggregator.GetEvent<ConnectionRemovedEvent>().Subscribe(CloseLibraryTabs);
        }
        private readonly IBrowserStateRepository _browserStateRepository;
        private readonly ISharedLibraryService _sharedLibraryService;

        /// <summary>
        /// Initializes the browser application state for a given user.
        /// </summary>
        /// <param name="user">The user.</param>
        public BrowserState InitializeState(User user)
        {
            if (user == null)
                user = User.Anonymous;

            State = _browserStateRepository.GetBrowserState(user) ?? NewBrowserState(user);
            var process = ((BrowserState) State).ProcessName;
            var now = DateTime.Now;
            SuiteState = SuiteStateRepository.GetSuiteState() ?? NewSuiteState(process, now);
            SuiteState.LastProcessAccess = process;
            SuiteState.ProcessAccessedOn = now;
            State.User.LastLogin = DateTime.Now;
            
            if (State.HasConnections())
                _sharedLibraryService.ConnectLibraries(State.Connections, user, false, true);

            SaveState();

            return (BrowserState) State;
        }

        /// <summary>
        /// Removes the anonymous browser state if the current user isn't the anonymous user.
        /// </summary>
        private void RemoveAnonymousState(object nothing)
        {
            RemoveAnonymousState();
        }

        /// <summary>
        /// Removes the anonymous browser state if the current user isn't the anonymous user.
        /// </summary>
        public void RemoveAnonymousState()
        {
            if (State.User.PersonId != User.Anonymous.PersonId)
                _browserStateRepository.DeleteAnonymousBrowserState();
        }

        /// <summary>
        /// Gets a new suite state.
        /// </summary>
        /// <param name="process">The process using state.</param>
        /// <param name="accessedOn">The time that state is accessed on.</param>
        private SuiteState NewSuiteState(string process, DateTime accessedOn)
        {
            var suiteState = new SuiteState
            {
                LastProcessAccess = process,
                ProcessAccessedOn = accessedOn
            };
            SuiteStateRepository.AddSuiteState(suiteState);
            SaveState();

            return suiteState;
        }

        /// <summary>
        /// Gets a new browser state
        /// </summary>
        /// <param name="user">The browser user.</param>
        private BrowserState NewBrowserState(User user)
        {
            var existingBrowserState = (BrowserState) State;
            var newBrowserState = new BrowserState();
            newBrowserState.SetUser(user);
            newBrowserState.Connections = LibraryConnectionRepository.ListLibraryConnections(true);
            newBrowserState.SidebarVisible = (existingBrowserState != null && existingBrowserState.SidebarVisible) && newBrowserState.HasConnections();
            _browserStateRepository.AddBrowserState(newBrowserState);

            return newBrowserState;
        }

        /// <summary>
        /// Saves the state.
        /// </summary>
        private void SaveState(ApplicationState state)
        {
            State = (BrowserState) state;
            SaveState();
        }
    }
}
