using System;
using System.IO;
using Bloom.Common;
using Bloom.Data.Interfaces;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Services
{
    /// <summary>
    /// Base class for all state services.
    /// </summary>
    public abstract class StateBaseService
    {
        /// <summary>
        /// Gets or sets the state data source.
        /// </summary>
        protected IDataSource StateDataSource { get; set; }

        /// <summary>
        /// Gets or sets the suite state repository.
        /// </summary>
        protected ISuiteStateRepository SuiteStateRepository { get; set; }

        /// <summary>
        /// Gets or sets the library connection repository.
        /// </summary>
        protected ILibraryConnectionRepository LibraryConnectionRepository { get; set; }

        /// <summary>
        /// Gets or sets the event aggregator.
        /// </summary>
        protected IEventAggregator EventAggregator { get; set; }

        /// <summary>
        /// Gets or sets the suite state.
        /// </summary>
        protected SuiteState SuiteState { get; set; }

        /// <summary>
        /// Gets or sets the application state.
        /// </summary>
        protected ApplicationState State { get; set; }

        /// <summary>
        /// Connects the data source.
        /// </summary>
        public void ConnectDataSource()
        {
            if (StateDataSource == null)
                throw new InvalidOperationException("Cannot connect to a null state data source.");

            if (string.IsNullOrEmpty(StateDataSource.FilePath))
                throw new InvalidOperationException("The file path to the state database file has not been specified.");

            if (File.Exists(StateDataSource.FilePath))
                StateDataSource.Connect();
            else
                StateDataSource.Create();
        }

        /// <summary>
        /// Gets the last process to access state.
        /// </summary>
        public ProcessType LastProcessToAccessState()
        {
            var suiteState = SuiteStateRepository.GetSuiteState();
            if (string.IsNullOrEmpty(suiteState?.LastProcessAccess))
                return ProcessType.None;

            var process = new BloomProcess(suiteState.LastProcessAccess);

            return process.Type;
        }

        /// <summary>
        /// Changes the state process.
        /// </summary>
        /// <param name="processType">The process type.</param>
        public void ChangeStateProcess(ProcessType processType)
        {
            var suiteState = SuiteStateRepository.GetSuiteState();
            var process = new BloomProcess(processType);
            suiteState.LastProcessAccess = process.Name;
            suiteState.ProcessAccessedOn = DateTime.Now;
            SaveState(); 
        }

        /// <summary>
        /// Saves the application state.
        /// </summary>
        public virtual void SaveState()
        {
            StateDataSource.Save();
        }
    }
}
