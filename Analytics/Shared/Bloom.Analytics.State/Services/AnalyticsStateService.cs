using System;
using Bloom.Data.Interfaces;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Analytics.State.Services
{
    /// <summary>
    /// Service for managing the analytics application state.
    /// </summary>
    public class AnalyticsStateService : TabbedStateBaseService, IAnalyticsStateService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsStateService" /> class.
        /// </summary>
        /// <param name="stateDataSource">The state data source.</param>
        /// <param name="analyticsStateRepository">The analytics state repository.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="tabRepository">The tab repository.</param>
        /// <param name="libraryService">The library service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public AnalyticsStateService(IDataSource stateDataSource, IAnalyticsStateRepository analyticsStateRepository, ILibraryConnectionRepository libraryConnectionRepository, ITabRepository tabRepository, ILibraryService libraryService, IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            StateDataSource = stateDataSource;
            TabRepository = tabRepository;
            _libraryConnectionRepository = libraryConnectionRepository;
            _analyticsStateRepository = analyticsStateRepository;
            _libraryService = libraryService;
        }
        private readonly ILibraryService _libraryService;
        private readonly IAnalyticsStateRepository _analyticsStateRepository;
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;

        /// <summary>
        /// Initializes the analytics application state.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The file path to the state database file has not been specified.</exception>
        public AnalyticsState InitializeState(User user)
        {
            State = _analyticsStateRepository.GetAnalyticsState(user) ?? AddNewState(user);

            if (State.User == null)
                return (AnalyticsState) State;

            State.User.LastLogin = DateTime.Now;
            if (State.Connections == null || State.Connections.Count <= 0)
                return (AnalyticsState) State;

            _libraryService.ConnectLibraries(State.Connections, user, false, true);

            return (AnalyticsState) State;
        }

        private AnalyticsState AddNewState(User user)
        {
            var analyticsState = new AnalyticsState();
            analyticsState.SetUser(user);
            analyticsState.Connections = _libraryConnectionRepository.ListLibraryConnections(true);
            _analyticsStateRepository.AddAnalyticsState(analyticsState);

            EventAggregator.GetEvent<UserChangedEvent>().Publish(null);
            return analyticsState;
        }
    }
}
