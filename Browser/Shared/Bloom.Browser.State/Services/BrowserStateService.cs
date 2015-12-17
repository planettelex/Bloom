using System;
using Bloom.Data.Interfaces;
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
        /// <param name="tabRepository">The tab repository.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public BrowserStateService(IDataSource stateDataSource, IBrowserStateRepository browserStateRepository, ITabRepository tabRepository, IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            StateDataSource = stateDataSource;
            TabRepository = tabRepository;
            _browserStateRepository = browserStateRepository;
        }
        private readonly IBrowserStateRepository _browserStateRepository;

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
            
            foreach (var libraryConnection in State.Connections)
                libraryConnection.Connect(State.User);

            return (BrowserState) State;
        }

        private BrowserState AddNewState(User user)
        {
            var browserState = new BrowserState();
            browserState.SetUser(user);
            _browserStateRepository.AddBrowserState(browserState);
            return browserState;
        }
    }
}
