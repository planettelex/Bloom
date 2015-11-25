using System;
using System.IO;
using Bloom.Data.Interfaces;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;

namespace Bloom.Analytics.State.Services
{
    /// <summary>
    /// Service for managing the analytics application state.
    /// </summary>
    public class AnalyticsStateService : IAnalyticsStateService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsStateService"/> class.
        /// </summary>
        /// <param name="stateDataSource">The state data source.</param>
        /// <param name="analyticsStateRepository">The analytics state repository.</param>
        public AnalyticsStateService(IDataSource stateDataSource, IAnalyticsStateRepository analyticsStateRepository)
        {
            _stateDataSource = stateDataSource;
            _analyticsStateRepository = analyticsStateRepository;
        }
        private readonly IDataSource _stateDataSource;
        private readonly IAnalyticsStateRepository _analyticsStateRepository;

        /// <summary>
        /// Initializes the analytics application state.
        /// </summary>
        /// <returns>
        /// The analytics application state.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">The file path to the state database file has not been specified.</exception>
        public AnalyticsState InitializeState()
        {
            if (string.IsNullOrEmpty(_stateDataSource.FilePath))
                throw new InvalidOperationException("The file path to the state database file has not been specified.");

            if (File.Exists(_stateDataSource.FilePath))
            {
                _stateDataSource.Connect();
                return _analyticsStateRepository.GetAnalyticsState() ?? AddNewAnalyticsState();
            }
            else
            {
                _stateDataSource.Create();
                return AddNewAnalyticsState();
            }
        }

        /// <summary>
        /// Saves the analytics application state.
        /// </summary>
        public void SaveState()
        {
            _stateDataSource.Save();
        }

        private AnalyticsState AddNewAnalyticsState()
        {
            var analyticsState = new AnalyticsState();
            _analyticsStateRepository.AddAnalyticsState(analyticsState);
            _stateDataSource.Save();
            return analyticsState;
        }
    }
}
