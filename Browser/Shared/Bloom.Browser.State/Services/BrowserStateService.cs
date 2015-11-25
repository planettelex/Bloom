using System;
using System.IO;
using Bloom.Data.Interfaces;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;

namespace Bloom.Browser.State.Services
{
    /// <summary>
    /// Service for managing the browser application state.
    /// </summary>
    public class BrowserStateService : IBrowserStateService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserStateService"/> class.
        /// </summary>
        /// <param name="stateDataSource">The state data source.</param>
        /// <param name="browserStateRepository">The browser state repository.</param>
        public BrowserStateService(IDataSource stateDataSource, IBrowserStateRepository browserStateRepository)
        {
            _stateDataSource = stateDataSource;
            _browserStateRepository = browserStateRepository;
        }
        private readonly IDataSource _stateDataSource;
        private readonly IBrowserStateRepository _browserStateRepository;

        /// <summary>
        /// Initializes the browser application state.
        /// </summary>
        /// <returns>
        /// The browser application state.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">The file path to the state database file has not been specified.</exception>
        public BrowserState InitializeState()
        {
            if (string.IsNullOrEmpty(_stateDataSource.FilePath))
                throw new InvalidOperationException("The file path to the state database file has not been specified.");

            if (File.Exists(_stateDataSource.FilePath))
            {
                _stateDataSource.Connect();
                return _browserStateRepository.GetBrowserState() ?? AddNewBrowserState();
            }
            else
            {
                _stateDataSource.Create();
                return AddNewBrowserState();
            }
        }

        private BrowserState AddNewBrowserState()
        {
            var browserState = new BrowserState();
            _browserStateRepository.AddBrowserState(browserState);
            _stateDataSource.Save();
            return browserState;
        }

        /// <summary>
        /// Saves the browser application state.
        /// </summary>
        public void SaveState()
        {
            _stateDataSource.Save();
        }
    }
}
